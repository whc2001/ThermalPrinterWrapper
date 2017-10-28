using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    [Flags]
    public enum TextStyle : byte
    {
        /// <summary>
        /// 反白
        /// </summary>
        INVERT = 0x01,

        /// <summary>
        /// 上下倒置
        /// </summary>
        UPSIDE_DOWN = 0x02,

        /// <summary>
        /// 加粗
        /// </summary>
        BOLD = 0x04,

        /// <summary>
        /// 双倍高度
        /// </summary>
        DOUBLE_HEIGHT = 0x08,

        /// <summary>
        /// 双倍宽度
        /// </summary>
        DOUBLE_WIDTH = 0x10,

        /// <summary>
        /// 删除线
        /// </summary>
        STROKE = 0x20
    }

    /// <summary>
    /// ESCPOS指令:设置打印字符模式
    /// </summary>
    public class EscPosSetTextStyle : EscPosCommand
    {
        public TextStyle Style { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x33, (byte)this.Style };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style">打印字符模式(位标识可叠加)</param>
        public EscPosSetTextStyle(TextStyle style)
        {
            this.Style = style;
        }
    }
}
