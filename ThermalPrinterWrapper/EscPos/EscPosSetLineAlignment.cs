using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper.EscPos
{
    /// <summary>
    /// 输出对齐方式
    /// </summary>
    public enum LineAlignment : byte
    {
        /// <summary>
        /// 左对齐
        /// </summary>
        LEFT = 0,

        /// <summary>
        /// 居中对齐
        /// </summary>
        MIDDLE = 1,

        /// <summary>
        /// 右对齐
        /// </summary>
        RIGHT = 2
    }

    /// <summary>
    /// ESCPOS指令:设置输出对齐方式
    /// </summary>
    public class EscPosSetLineAlignment : EscPosCommand
    {
        public LineAlignment Alignment { get; set; }

        public override byte[] GetBytes()
        {
            return new byte[] { ESC, 0x61, (byte)this.Alignment };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alignment">输出对齐方式</param>
        public EscPosSetLineAlignment(LineAlignment alignment)
        {
            this.Alignment = alignment;
        }
    }
}
