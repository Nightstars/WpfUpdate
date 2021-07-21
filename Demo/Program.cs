using SmartSoft.common.Utils.solution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        //static async Task Main(string[] args)
        static void Main(string[] args)
        {
            //using var httpClient = new HttpClient();
            //var url = "http://192.168.2.114:10002/upload/smarttools/2021/7/2021715003/Update_20210715491258281.zip";
            //byte[] imageBytes = await httpClient.GetByteArrayAsync(url).ConfigureAwait(false);
            ////var responseMessage = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false)
            //string localFilename = "update.zip";
            //string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localFilename);
            //File.WriteAllBytes(localPath, imageBytes);

            List<SolutionInfo> list= new SolutionUtil(@"D:\myworkspace\V6\v6-xinjin\Longnows.Saas.XinJing.sln").SlnParse();
            Console.Read();
        }
    }
}
