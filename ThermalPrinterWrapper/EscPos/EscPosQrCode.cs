using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// ESCPOS指令:二维码
    /// </summary>
    public class EscPosQrCode : EscPosCommand
    {
        public string Content { get; set; }

        public override byte[] GetBytes()
        {
            List<byte> list = new List<byte>();
            list.AddRange(new byte[] { GS, 0x28, 0x6B, (byte)(this.Content.Length + 3), 0x00, 0x31, 0x50, 0x30 });
            list.AddRange(Encoding.UTF8.GetBytes(Content));
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">二维码内容(最长128字节)</param>
        public EscPosQrCode(string content)
        {
            if (content.Length > 128)
                throw new ArgumentOutOfRangeException("content", "数据超长, 最长128字节");
            this.Content = content;
        }
    }
}
