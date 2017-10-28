using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalPrinterWrapper
{
    public abstract class Printer
    {
        /// <summary>
        /// 已连接
        /// </summary>
        public abstract bool Connected { get; }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        public abstract void WriteRaw(byte[] data);

        /// <summary>
        /// 发送ESC/POS指令
        /// </summary>
        /// <param name="commands">ESC/POS指令</param>
        public abstract void WriteEscPos(params EscPos.EscPosCommand[] commands);

        /// <summary>
        /// 连接
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// 断开连接
        /// </summary>
        public abstract void Disconnect();
    }
}
