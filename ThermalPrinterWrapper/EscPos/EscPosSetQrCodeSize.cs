using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置二维码尺寸
    /// </summary>
    public class EscPosSetQrCodeSize : EscPosCommand
    {
        public byte Size { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { GS, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, this.Size };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">二维码尺寸(1-16)</param>
        public EscPosSetQrCodeSize(byte size)
        {
            if (size < 1 || size > 16)
                throw new ArgumentOutOfRangeException("二维码尺寸只能在1-16之间");
            this.Size = size;
        }
    }
}
