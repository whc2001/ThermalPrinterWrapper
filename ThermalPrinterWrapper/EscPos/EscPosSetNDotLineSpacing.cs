using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置行间距为 n 点行
    /// </summary>
    public class EscPosSetNDotLineSpacing : EscPosCommand
    {
        public byte N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x33, this.N };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">行间距点行数(0-255)</param>
        public EscPosSetNDotLineSpacing(byte n)
        {
            this.N = n;
        }
    }
}
