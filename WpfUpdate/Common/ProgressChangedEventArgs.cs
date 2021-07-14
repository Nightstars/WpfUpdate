using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.Common
{
    public class ProgressChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 进度状态
        /// </summary>
        public ProgressType Type { get; set; }

        /// <summary>
        /// 进度
        /// </summary>
        public double ProgressValue { get; set; }

        /// <summary>
        /// 已下载文件大小
        /// </summary>
        public double ReceivedSize { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double? TotalSize { get; set; }

        public string Message { get; set; }
    }
}
