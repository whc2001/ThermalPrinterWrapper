using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:打印行缓冲区里的内容，并向前走纸 n 点行。
    /// </summary>
    public class EscPosLineFeedNDotLines : EscPosCommand
    {
        public byte N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x4A, this.N };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">走纸点行数(0-255)</param>
        public EscPosLineFeedNDotLines(byte n)
        {
            this.N = n;
        }
    }
}
