using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// 条码基本线条宽度
    /// </summary>
    public enum BarcodeLineWidth : byte
    {
        /// <summary>
        /// 窄
        /// </summary>
        NARROW = 2,

        /// <summary>
        /// 宽
        /// </summary>
        WIDE = 3,
    }

    /// <summary>
    /// ESCPOS指令:设置条形码宽度
    /// </summary>
    public class EscPosSetBarcodeLineWidth : EscPosCommand
    {
        public BarcodeLineWidth Width { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { GS, 0x77, (byte)this.Width };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">条码基本线条宽度</param>
        public EscPosSetBarcodeLineWidth(BarcodeLineWidth width)
        {
            this.Width = width;
        }
    }
}
