using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Update.Common.http
{
    public class DownloadService
    {
        /// <summary> 下载文件 </summary>
        /// <param name="URL">下载文件地址</param>
        /// <param name="Filename">下载后的存放地址</param>
        /// <param name="Prog">用于显示的进度条</param>
        ///
        public static void DownloadFile(string URL, string filename, Action<string, string> percentAction = null, int refreshTime = 1000)
        {
            float percent = 0;
            int total = 0;
            int current = 0;
            HttpWebRequest Myrq = HttpWebRequest.Create(URL) as HttpWebRequest;
            Myrq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
            //Myrq.Headers.Add("Token", Token);
            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();

            long totalBytes = myrp.ContentLength;
            total = (int)totalBytes;
            Stream st = myrp.GetResponseStream();
            if (File.Exists(filename))
            {
                ///生成时间戳
                int index = filename.LastIndexOf('.');
                string insertStr = "[" + System.Text.RegularExpressions.Regex.Replace(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), @"[^\d]*", "") + "]";
                filename = filename.Insert(index, insertStr);
            }


            Stream so = new FileStream(filename, FileMode.Create);

            long totalDownloadedByte = 0;
            byte[] by = new byte[1024];

            int osize = st.Read(by, 0, (int)by.Length);


            // Todo ：定时刷新进度
            if (percentAction != null)
            {
                Action action = () =>
                {
                    while (true)
                    {
                        Thread.Sleep(refreshTime);

                        // Todo ：返回进度
                        percentAction(current.ToString(), total.ToString());

                        if (current == total) break;
                    }
                };

                Task task = new Task(action);
                task.Start();
            }


            while (osize > 0)
            {
                totalDownloadedByte = osize + totalDownloadedByte;
                so.Write(by, 0, osize);
                current = (int)totalDownloadedByte;

                osize = st.Read(by, 0, (int)by.Length);

                percent = (float)totalDownloadedByte / (float)totalBytes * 100;
            }
            so.Close();
            st.Close();
        }



    }
}
