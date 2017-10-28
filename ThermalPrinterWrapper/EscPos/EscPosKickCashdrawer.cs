using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:钱箱控制脉冲
    /// </summary>
    public class EscPosKickCashdrawer : EscPosCommand
    {
        public byte OnDutation { get; set; }
        public byte OffDutation { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x70, 0x00, this.OnDutation, this.OffDutation };
        }

        public EscPosKickCashdrawer()
        {
            this.OnDutation = 0x3C;
            this.OffDutation = 0xFF;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onDuration">钱箱信号开启时间</param>
        /// <param name="offDuration">钱箱信号关闭时间</param>
        public EscPosKickCashdrawer(byte onDuration, byte offDuration)
        {
            this.OnDutation = onDuration;
            this.OffDutation = offDuration;
        }
    }
}
