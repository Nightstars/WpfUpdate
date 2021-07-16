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
    public static class HttpClientExtensions
    {
        private const int BufferSize = 8192;

        public static async void GetByteArrayAsync(this HttpClient client, Uri requestUri, IProgress<HttpDownloadProgress> progress, CancellationToken cancellationToken=default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            using (var responseMessage = await client.GetAsync(requestUri).ConfigureAwait(false))
            {
                responseMessage.EnsureSuccessStatusCode();

                var content = responseMessage.Content;
                if (content == null)
                {
                    //return Array.Empty<byte>();
                    return;
                }

                var headers = content.Headers;

                if (!Directory.Exists("./temp"))
                {
                    Directory.CreateDirectory("./temp");
                }

                var fileinfo = new FileInfo("./temp/Update.zip");
                if (fileinfo.Exists)
                {
                    fileinfo.Delete();
                }

                var contentLength = headers.ContentLength;
                //using var fileStream = file.Create();
                using (var responseStream = await content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    var buffer = new byte[(int)contentLength/100];
                    int bytesRead;
                    //var bytes = new List<byte>();

                    var downloadProgress = new HttpDownloadProgress();
                    if (contentLength.HasValue)
                    {
                        downloadProgress.TotalBytesToReceive = (ulong)contentLength.Value;
                    }
                    progress?.Report(downloadProgress);

                    using var filestream = new FileStream("./temp/Update.zip", FileMode.Append, FileAccess.Write);

                    while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length/800, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        //bytes.AddRange(buffer.Take(bytesRead));

                        filestream.Write(buffer, 0, bytesRead);

                        downloadProgress.BytesReceived += (ulong)bytesRead;

                        progress?.Report(downloadProgress);
                    }




                    //return bytes.ToArray();
                }
            }
        }
    }
}
