using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            var url = "http://192.168.216.1/Update.zip";
            byte[] imageBytes = await httpClient.GetByteArrayAsync(url);

            string localFilename = "update.zip";
            string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localFilename);
            File.WriteAllBytes(localPath, imageBytes);
            Console.Read();
        }
    }
}
