using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:打印行缓冲器里的内容并向前走纸一行，当行缓冲器为空时只向前走纸一行。
    /// </summary>
    public class EscPosLineFeed : EscPosCommand
    {
        public override byte[] GetBytes()
        {
            return new byte[] { 0x0A };
        }
    }
}
