using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.Common
{
    public enum ProgressType
    {
        /// <summary>
        /// 检查更新
        /// </summary>
        Check,
        /// <summary>
        /// 下载更新包
        /// </summary>
        Donwload,
        /// <summary>
        /// 更新文件
        /// </summary>
        Updatefile,
        /// <summary>
        /// 更新完成
        /// </summary>
        Done,
        /// <summary>
        /// 更新失败
        /// </summary>
        Fail
    }
}