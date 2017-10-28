using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:打印机初始化
    /// </summary>
    public class EscPosInitialize : EscPosCommand
    {
        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x40 };
        }
    }
}
