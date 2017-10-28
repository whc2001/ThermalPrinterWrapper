//https://support.microsoft.com/en-us/help/322091/how-to-send-raw-data-to-a-printer-by-using-visual-c--net

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ThermalPrinterWrapper.EscPos;

namespace ThermalPrinterWrapper
{
    public class UsbPrinter : Printer
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }
        [DllImport("winspool.drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        private IntPtr printerHandle = new IntPtr(0);

        public string PrinterName { get; set; }

        public override bool Connected => printerHandle != IntPtr.Zero;

        public void SendBytesToPrinter(string szPrinterName, byte[] data)
        {
            int dwWritten = 0;
            IntPtr dataPtr = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            di.pDocName = "EscPosData";
            di.pDataType = "RAW";

            dataPtr = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, dataPtr, data.Length);

            StartDocPrinter(printerHandle, 1, di);
            StartPagePrinter(printerHandle);
            bool success = WritePrinter(printerHandle, dataPtr, data.Length, out dwWritten);

            bool endPageSuccess = EndPagePrinter(printerHandle);
            bool endDocSuccess = EndDocPrinter(printerHandle);
            Marshal.FreeCoTaskMem(dataPtr);

            if(!success)
                throw new InvalidOperationException("设备未就绪, 不能写入");
        }

        public override void Connect()
        {
            if (false)
                throw new InvalidOperationException("打开失败: 打印机不存在或离线");
            else
                OpenPrinter(PrinterName.Normalize(), out printerHandle, IntPtr.Zero);
        }

        public override void Disconnect()
        {
            if (Connected)
                ClosePrinter(printerHandle);
            printerHandle = IntPtr.Zero;
        }

        public override void WriteEscPos(params EscPosCommand[] commands)
        {
            List<byte> bytes = new List<byte>();
            foreach (EscPosCommand cmd in commands)
                bytes.AddRange(cmd.GetBytes());
            WriteRaw(bytes.ToArray());
        }

        public override void WriteRaw(byte[] data)
        {
            SendBytesToPrinter(PrinterName, data);
        }
    }
}