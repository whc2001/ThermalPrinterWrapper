using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:打印缓冲区里的数据，如果有黑标功能，打印后进纸到下一个黑标位置。
    /// </summary>
    public class EscPosLineFeedToLabelMark : EscPosCommand
    {
        public override byte[] GetBytes()
        {
            return new byte[] { 0x0C };
        }
    }
}
