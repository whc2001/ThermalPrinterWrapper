using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// 条码类型
    /// </summary>
    public enum BarcodeType : byte
    {
        UPC_A = 0,
        UPC_E = 1,
        EAN13 = 2,
        EAN8 = 3,
        CODE39 = 4,
        I25 = 5,
        CODEBAR = 6,
        CODE93 = 7,
        CODE128 = 8,
        CODE11 = 9,
        MSI = 10,
    }

    /// <summary>
    /// ESCPOS指令:条形码
    /// </summary>
    public class EscPosBarcode : EscPosCommand
    {
        public BarcodeType Type { get; set; }

        public string Content { get; set; }

        private bool IsContentValid(BarcodeType type, string content)
        {
            switch(type)
            {
                case BarcodeType.UPC_A:
                case BarcodeType.UPC_E:
                    return (content.Length == 11 || content.Length == 12) && (content.All(i => i >= 48 && i <= 57));
                case BarcodeType.EAN13:
                    return (content.Length == 12 || content.Length == 13) && (content.All(i => i >= 48 && i <= 57));
                case BarcodeType.EAN8:
                    return (content.Length == 7 || content.Length == 8) && (content.All(i => i >= 48 && i <= 57));
                case BarcodeType.CODE39:
                    return content.All(i => (i >= 45 && i <= 57) || (i >= 65 && i <= 90) || i == 32 || i == 36 || i == 37 || i == 43);
                case BarcodeType.I25:
                    return (content.Length % 2 == 0) && (content.All(i => i >= 48 && i <= 57));
                case BarcodeType.CODEBAR:
                    return content.All(i => (i >= 45 && i <= 58) || (i >= 65 && i <= 68) || i == 36 || i == 43);
                case BarcodeType.CODE93:
                case BarcodeType.CODE128:
                    return content.All(i => i >= 0 && i <= 127);
                case BarcodeType.CODE11:
                case BarcodeType.MSI:
                    return content.All(i => i >= 48 && i <= 57);
            }
            return false;
        }

        public override byte[] GetBytes()
        {
            List<byte> list = new List<byte>();
            list.AddRange(new byte[] { GS, 0x6B, (byte)this.Type });
            list.AddRange(Encoding.ASCII.GetBytes(Content));
            list.Add(0x00);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">条码类型</param>
        /// <param name="content">条码内容</param>
        public EscPosBarcode(BarcodeType type, string content)
        {
            if (!IsContentValid(type, content))
                throw new ArgumentException("条码内容不符合选择类型的规定");
            this.Type = type;
            this.Content = content;
        }
    }
}
