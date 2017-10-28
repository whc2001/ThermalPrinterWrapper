using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using ThermalPrinterWrapper.EscPos;

namespace ThermalPrinterWrapper
{
    public class SerialPrinter : Printer
    {
        private SerialPort serial;

        /// <summary>
        /// 串行端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baud { get; set; }

        /// <summary>
        /// 已连接
        /// </summary>
        public override bool Connected => serial != null && serial.IsOpen;

        /// <summary>
        /// 获得所有可用串行端口
        /// </summary>
        /// <returns></returns>
        public string[] GetSerialPortNames()
            => SerialPort.GetPortNames();

        /// <summary>
        /// 连接设备
        /// </summary>
        public override void Connect()
        {
            if (serial.IsOpen)
                throw new InvalidOperationException("已连接, 请先断开");
            if (string.IsNullOrEmpty(Port))
                throw new ArgumentNullException("Port", "端口名称为空");
            if(Baud <= 0)
                throw new ArgumentOutOfRangeException("Baud", "波特率必须大于等于0");
            serial.PortName = Port;
            serial.BaudRate = Baud;
            serial.Open();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public override void Disconnect()
        {
            serial.Dispose();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        public override void WriteRaw(byte[] data)
        {
            try
            {
                serial.Write(data, 0, data.Length);
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

        public SerialPrinter()
        {
            serial = new SerialPort();
        }

        public SerialPrinter(string port) : this()
        {
            Port = port;
        }

        public SerialPrinter(string port, int baud) : this(port)
        {
            Baud = baud;
        }
    }
}
