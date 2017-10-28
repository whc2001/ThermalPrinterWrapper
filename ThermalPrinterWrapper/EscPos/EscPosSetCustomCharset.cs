using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:选择自定义字符集
    /// </summary>
    public class EscPosSetCustomCharset : EscPosCommand
    {
        public bool UseCustomCharset { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x25, Convert.ToByte(this.UseCustomCharset) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useCustomCharset">是否使用自定义字符集</param>
        public EscPosSetCustomCharset(bool useCustomCharset)
        {
            this.UseCustomCharset = useCustomCharset;
        }
    }
}
