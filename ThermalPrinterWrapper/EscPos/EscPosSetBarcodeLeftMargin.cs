using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置条形码打印的左边距
    /// </summary>
    public class EscPosSetBarcodeLeftMargin : EscPosCommand
    {
        public byte N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { GS, 0x78, this.N };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">左边距单位(0-255)</param>
        public EscPosSetBarcodeLeftMargin(byte n)
        {
            this.N = n;
        }
    }
}
