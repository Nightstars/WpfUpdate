using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.Common
{
    public class DownloadStatisticsEventArgs : EventArgs
    {

        public DateTime Remaining { get; set; }

        public string Speed { get; set; }
    }
}
