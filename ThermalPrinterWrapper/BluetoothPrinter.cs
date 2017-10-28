using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net.Ports;
using InTheHand.Net;
using System.IO;
using ThermalPrinterWrapper.EscPos;

namespace ThermalPrinterWrapper
{
    /// <summary>
    /// 蓝牙打印机
    /// </summary>
    public class BluetoothPrinter : Printer
    {
        private BluetoothAddress address;
        private BluetoothDeviceInfo device;
        private BluetoothClient client;
        private Stream clientStream;

        /// <summary>
        /// 选择的设备地址
        /// </summary>
        public byte[] Address { get; set; }

        public override bool Connected => client != null && client.Connected;

        //实例化BluetoothClient
        private void InitClient()
        {
            if (client != null)
                return;
            try
            {
                client = new BluetoothClient();
            }
            catch
            {
                throw new NotSupportedException("无法找到支持的蓝牙软件栈, 请考虑使用微软自带的蓝牙驱动");
            }
        }

        /// <summary>
        /// 搜索设备
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, byte[]> DiscoverDevice()
        {
            InitClient();
            BluetoothDeviceInfo[] devices = client.DiscoverDevices(int.MaxValue, true, true, true, false);
            return devices.ToDictionary(k => k.DeviceName, v => v.DeviceAddress.ToByteArray());
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        public override void Connect()
        {
            if (Address == null)
                throw new InvalidOperationException("设备地址为空");
            Connect(Address);
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="deviceAddress">设备地址</param>
        /// <param name="pin">设备PIN码</param>
        public void Connect(byte[] deviceAddress, string pin = "0000")
        {
            if (client != null && client.Connected)
                throw new InvalidOperationException("已连接, 请先断开");
            Address = deviceAddress;
            address = new BluetoothAddress(Address);
            device = new BluetoothDeviceInfo(address);
            if (device == null)
                throw new ArgumentException("无法找到指定地址的蓝牙设备");
            try
            {
                device.SetServiceState(BluetoothService.SerialPort, true);
            }
            catch
            {
                throw new InvalidOperationException("设备不支持SPP连接方式");
            }
            if (!device.Authenticated)
                if (!BluetoothSecurity.PairRequest(address, pin))
                    throw new InvalidOperationException("配对失败");
            try
            {
                client.Connect(address, BluetoothService.SerialPort);
                clientStream = client.GetStream();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("连接失败");
            }
        }

        /// <summary>
        /// 断开设备
        /// </summary>
        public override void Disconnect()
        {
            if (clientStream != null)
            {
                clientStream.Dispose();
                clientStream = null;
            }
            if (client != null)
            {
                client.Close();
                client.Dispose();
            }
            client = null;
            device = null;
            address = null;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        public override void WriteRaw(byte[] data)
        {
            try
            {
                clientStream.Write(data, 0, data.Length);
                clientStream.Flush();
            }
            catch
            {
                Disconnect();
                throw new InvalidOperationException("设备未就绪, 不能写入");
            }
        }

        /// <summary>
        /// 发送ESC/POS指令
        /// </summary>
        /// <param name="commands">ESC/POS指令</param>
        public override void WriteEscPos(params EscPosCommand[] commands)
        {
            List<byte> bytes = new List<byte>();
            foreach (EscPosCommand cmd in commands)
                bytes.AddRange(cmd.GetBytes());
            WriteRaw(bytes.ToArray());
        }

        public BluetoothPrinter()
        {
            InitClient();
        }
    }
}
