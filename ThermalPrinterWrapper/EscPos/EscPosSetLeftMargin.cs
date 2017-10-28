using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置左边空白点数, 单位 0.125mm
    /// </summary>
    public class EscPosSetLeftMargin : EscPosCommand
    {
        public ushort N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x33, (byte)(this.N & 0xFF), (byte)(this.N >> 8) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">左边距单位(0-65535)</param>
        public EscPosSetLeftMargin(ushort n)
        {
            this.N = n;
        }
    }
}
