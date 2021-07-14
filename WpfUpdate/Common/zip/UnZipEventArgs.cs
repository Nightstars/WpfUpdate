using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.Common.zip
{
    public class UnZipEventArgs
    {
        public long Size { get; set; }

        public int Index { get; set; }

        public int Count { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }
    }
}
