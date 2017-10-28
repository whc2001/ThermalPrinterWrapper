using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:打印行缓冲区里的内容，并向前走纸 n 行。
    /// </summary>
    public class EscPosLineFeedNLines : EscPosCommand
    {
        public byte N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x64, this.N };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">走纸行数(0-255)</param>
        public EscPosLineFeedNLines(byte n)
        {
            this.N = n;
        }
    }
}
