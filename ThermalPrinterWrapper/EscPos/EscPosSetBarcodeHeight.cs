using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置条形码高度
    /// </summary>
    public class EscPosSetBarcodeHeight : EscPosCommand
    {
        public byte N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { GS, 0x68, this.N };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">条码高度(1-255)</param>
        public EscPosSetBarcodeHeight(byte n)
        {
            if (n == 0)
                throw new ArgumentOutOfRangeException("条码高度不能为0");
            this.N = n;
        }
    }
}
