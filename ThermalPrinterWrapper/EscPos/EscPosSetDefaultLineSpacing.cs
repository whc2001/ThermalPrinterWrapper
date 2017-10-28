using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:设置行间距为 1/6 英寸(4 毫米,32 点)
    /// </summary>
    public class EscPosSetDefaultLineSpacing : EscPosCommand
    {
        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x32 };
        }
    }
}
