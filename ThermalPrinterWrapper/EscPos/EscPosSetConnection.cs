using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:连线打印机
    /// </summary>
    public class EscPosSetConnection : EscPosCommand
    {
        public bool Connect { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x3D, Convert.ToByte(Connect) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connect">打印机是否连线</param>
        public EscPosSetConnection(bool connect)
        {
            this.Connect = connect;
        }
    }
}
