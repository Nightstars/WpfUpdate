using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.Common
{
    public struct HttpDownloadProgress
    {
        public ulong BytesReceived { get; set; }

        public ulong? TotalBytesToReceive { get; set; }
    }
}
