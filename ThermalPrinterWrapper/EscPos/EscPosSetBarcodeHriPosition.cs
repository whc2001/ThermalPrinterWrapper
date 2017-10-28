using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// 条码文本(HRI)位置
    /// </summary>
    public enum BarcodeHriPosition : byte
    {
        /// <summary>
        /// 不打印
        /// </summary>
        DISABLED = 0,

        /// <summary>
        /// 在条码上方
        /// </summary>
        ABOVE = 1,

        /// <summary>
        /// 在条码下方
        /// </summary>
        BELOW = 2,

        /// <summary>
        /// 在条码上方和下方
        /// </summary>
        ABOVE_AND_BELOW = 3
    }

    /// <summary>
    /// ESCPOS指令:设定条码对应的字符(HRI)打印方式
    /// </summary>
    public class EscPosSetBarcodeHriPosition : EscPosCommand
    {
        public BarcodeHriPosition Position { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { GS, 0x48, (byte)this.Position };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">条码位置</param>
        public EscPosSetBarcodeHriPosition(BarcodeHriPosition position)
        {
            this.Position = position;
        }
    }
}
