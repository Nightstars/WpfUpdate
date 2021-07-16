using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Update.Common.net;

namespace Update.Common
{
    class HttpUtil
    {
        public delegate void DownloadStatisticsEventHandler(object sender, DownloadStatisticsEventArgs e);
        /// <summary>
        /// 下载统计
        /// </summary>
        public event DownloadStatisticsEventHandler DownloadStatistics;

        public async Task DownloadFile(string url, FileInfo file)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);


                var n = response.Content.Headers.ContentLength;
                var stream = await response.Content.ReadAsStreamAsync();

                using (var fileStream = file.Create())
                using (stream)
                {
                    byte[] buffer = new byte[1024*50];
                    var readLength = 0;
                    int length;
                    DateTime _startTime=DateTime.Now;
                    while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        readLength += length;

                        //var interval = DateTime.Now - _startTime;

                        //var downLoadSpeed = interval.Seconds < 1
                        //    ? NetUtil.ToUnit(readLength - (readLength - length))
                        //    : NetUtil.ToUnit(readLength - (readLength - length) / interval.Seconds);

                        //var size = (n - readLength) / (1024 * 1024);
                        //var remainingTime = new DateTime().AddSeconds(Convert.ToDouble(size));

                        var args = new DownloadStatisticsEventArgs();
                        //args.Remaining = remainingTime;
                        args.Speed = Math.Round((double)(((double)readLength) / n * 100), 2).ToString();
                        DownloadStatistics.Invoke(this, args);

                        // 写入到文件
                        fileStream.Write(buffer, 0, length);

                        _startTime = DateTime.Now;
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
