using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Update.Common;
using Update.Common.http;
using Update.Common.zip;
using Update.ViewModel;

namespace WpfUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Update : Window
    {
        //UpdateVM updateVM=new UpdateVM ();
        public Update()
        {
            InitializeComponent();
            //param.Text = arg;
            //var paramter = arg.Split('|');
            //container.DataContext = updateVM;
            //updateVM._progressValuelue = 20;
            //while (updateVM._progressValuelue < 100)
            //{
            //    updateVM._progressValuelue++;
            //}

            //var process = new Progress<HttpDownloadProgress>();
            //HttpClient httpClient = new HttpClient();
            //HttpClientExtensions.GetByteArrayAsync(httpClient, new Uri(paramter[3]), process);

            //process.ProgressChanged += (obj, e) =>
            //{
            //    progress.Text = ((int.Parse(e.BytesReceived.ToString()) / int.Parse(e.TotalBytesToReceive.ToString())) * 100).ToString();
            //    progressBar.Value = (double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100);
            //    updateVM._progressValuelue = (double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100);
            //};
            //progress.Text = $"下载速度：0，剩余时间：xxx";

            //this.download(paramter[3]);



        }

        //public void download(string arg)
        //{
        //    if (!Directory.Exists("./temp"))
        //    {
        //        Directory.CreateDirectory("./temp");
        //    }

        //    var fileinfo = new FileInfo("./temp/Update.zip");
        //    if (fileinfo.Exists)
        //    {
        //        fileinfo.Delete();
        //    }

        //    HttpUtil httpUtil = new HttpUtil();
        //    httpUtil.DownloadStatistics += Bootstrap_DownloadStatistics;
        //    httpUtil.DownloadFile(arg, new FileInfo("./temp/Update.zip")).Wait();
        //}

        //private void Bootstrap_DownloadStatistics(object sender, DownloadStatisticsEventArgs e)
        //{
        //    Debug.WriteLine(e.Speed);
        //    updateVM._progressValuelue = double.Parse(e.Speed);
        //    progressBar.Value = double.Parse(e.Speed);
        //    progress.Text = $"下载速度：{e.Speed}，剩余时间：{e.Remaining.Minute}:{e.Remaining.Second}";
        //}

        private void OnUnzip(object sender, UnZipEventArgs e)
        {
            //zipProgress.Text = $"当前第{e.Index}个 文件数：{e.Count} 文件名：{e.Name}";

            //Debug.WriteLine($"当前第{e.Index}个 文件数：{e.Count} 文件名：{e.Name}");
        }

        //private void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    //if (progress.Value == 100)
        //    //{
        //    //    ZipUtil zipUtil = new ZipUtil();
        //    //    zipUtil.UnZipEvent += OnUnzip;
        //    //    var result=zipUtil.UnZip("./temp/Update.zip", "./");
        //    //    if (result)
        //    //    {
        //    //        StartMain("WpfMainApp");
        //    //    }
        //    //}
        //}
        public bool StartMain(string appName)
        {
            try
            {
                Process.Start($"./{appName}.exe");
                Process.GetCurrentProcess().Kill();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string Log { get; set; }
        private void Download_Click(object sender, RoutedEventArgs e)
        {
            string URL = @"http://192.168.0.103:8080/Update.zip";
            if (string.IsNullOrEmpty(URL))
            {
                this.Log = "请求的下载地址是空，请检查！";
                logTextBlock.Text = Log;
                return;
            }
            this.updataGrid.Visibility = Visibility.Collapsed;
            this.progressGrid.Visibility = Visibility.Visible;
            string save = "./temp";

            if (!Directory.Exists(save))
            {
                Directory.CreateDirectory(save);
            }

            string fileName = System.IO.Path.GetFileName(URL);

            string savePath = System.IO.Path.Combine(save, fileName);

            Action<string, string> action = (current, total) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Log = $"正在下载...{FormatBytes(long.Parse(current)).PadLeft(10, ' ')} / {FormatBytes(long.Parse(total))}";
                    logTextBlock.Text = Log;
                    this.progress.Value = (int)((double.Parse(current) / double.Parse(total)) * 100);

                    if (current == total)
                    {
                        this.progress.Value = 100;

                        Task.Delay(1000).ContinueWith(l =>
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                this.Log = $"下载完成！";
                                logTextBlock.Text = Log;
                                this.confirmGrid.Visibility = Visibility.Collapsed;
                                this.unzipGrid.Visibility = Visibility.Visible;
                                ZipUtil zipUtil = new ZipUtil();
                                zipUtil.UnZipEvent += OnUnzip;
                                var result = zipUtil.UnZip("./temp/Update.zip", "./");
                                if (result)
                                {
                                    if (StartMain("WpfMainApp"))
                                    {
                                        this.Close();
                                    }
                                }
                            });

                        });

                        this.confirmGrid.Visibility = Visibility.Visible;
                        this.updataGrid.Visibility = Visibility.Collapsed;
                        this.progressGrid.Visibility = Visibility.Collapsed;

                    }
                });

            };

            Task.Run(() =>
            {
                DownloadService.DownloadFile(URL, savePath, action, 1000);
            });
        }

        public static string FormatBytes(long bytes)
        {
            string[] Suffix = { "Byte", "KB", "MB", "GB", "TB" };
            int i = 0;
            double dblSByte = bytes;

            if (bytes > 1024)
                for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                    dblSByte = bytes / 1024.0;

            return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.confirmGrid.Visibility = Visibility.Collapsed;
            this.unzipGrid.Visibility = Visibility.Visible;
            ZipUtil zipUtil = new ZipUtil();
            zipUtil.UnZipEvent += OnUnzip;
            var result = zipUtil.UnZip("./temp/Update.zip", "./");
            if (result)
            {
                StartMain("WpfMainApp");
            }

            this.Close();
        }


    }
}
