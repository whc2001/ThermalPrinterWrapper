using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    public abstract class EscPosCommand
    {
        public const byte ESC = 0x1B;

        public const byte GS = 0x1D;

        public readonly Encoding GBK = Encoding.GetEncoding(936);

        public abstract byte[] GetBytes();
    }
}
