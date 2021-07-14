using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Update.Common
{
    class HttpUtil
    {
        public delegate void DownloadStatisticsEventHandler(object sender, DownloadStatisticsEventArgs e);
        /// <summary>
        /// 下载统计
        /// </summary>
        public event DownloadStatisticsEventHandler DownloadStatistics;

        //public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
        ///// <summary>
        ///// 进度更新
        ///// </summary>
        //public event ProgressChangedEventHandler ProgressChanged;

        public async Task DownloadFile(string url, FileInfo file)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            try
            {
                var n = response.Content.Headers.ContentLength;
                var stream = await response.Content.ReadAsStreamAsync();
                using (var fileStream = file.Create())
                using (stream)
                {
                    byte[] buffer = new byte[1024];
                    var readLength = 0;
                    int length;
                    while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        readLength += length;

                        //var interval = DateTime.Now - _startTime;

                        //var downLoadSpeed = interval.Seconds < 1
                        //    ? StatisticsUtil.ToUnit(Packet.ReceivedBytes - BeforBytes)
                        //    : StatisticsUtil.ToUnit(Packet.ReceivedBytes - BeforBytes / interval.Seconds);

                        //var size = (Packet.TotalBytes - Packet.ReceivedBytes) / (1024 * 1024);
                        //var remainingTime = new DateTime().AddSeconds(Convert.ToDouble(size));

                        //var args = new DownloadStatisticsEventArgs();
                        //args.Remaining = remainingTime;
                        //args.Speed = downLoadSpeed;
                        //DownloadStatistics.BeginInvoke(this, args, null, null);

                        //_startTime = DateTime.Now;
                        //BeforBytes = Packet.ReceivedBytes;
                        Console.WriteLine("下载进度" + ((double)readLength) / n * 100);

                        Thread.Sleep(1000);
                        var args = new DownloadStatisticsEventArgs();
                        args.Remaining = DateTime.Now;
                        args.Speed = (((double)readLength) / n * 100).ToString();
                        DownloadStatistics.Invoke(this, args);

                        // 写入到文件
                        fileStream.Write(buffer, 0, length);
                    }
                }

            }
            catch (Exception e)
            {
            }
        }
    }
}
