using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置字符间距
    /// </summary>
    public class EscPosSetCharacterSpacing : EscPosCommand
    {
        public byte N { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x20, this.N };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">字符间距(0-255)</param>
        public EscPosSetCharacterSpacing(byte n)
        {
            this.N = n;
        }
    }
}
