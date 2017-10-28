using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// 普通文本
    /// </summary>
    public class EscPosPlainString : EscPosCommand
    {
        public Encoding SourceEncoding { get; set; }
        public Encoding DestinationEncoding { get; set; }
        public string StringContent { get; set; }

        public override byte[] GetBytes()
        {
            return Encoding.Convert(SourceEncoding, DestinationEncoding, SourceEncoding.GetBytes(StringContent));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">文本字串</param>
        public EscPosPlainString(string str)
        {
            this.StringContent = str;
            this.SourceEncoding = Encoding.Default;
            this.DestinationEncoding = GBK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">文本字串</param>
        /// <param name="source">源编码</param>
        public EscPosPlainString(string str, Encoding source)
        {
            this.StringContent = str;
            this.SourceEncoding = source;
            this.DestinationEncoding = GBK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">文本字串</param>
        /// <param name="source">源编码</param>
        /// <param name="destination">目标编码</param>
        public EscPosPlainString(string str, Encoding source, Encoding destination)
        {
            this.StringContent = str;
            this.SourceEncoding = source;
            this.DestinationEncoding = destination;
        }
    }
}
