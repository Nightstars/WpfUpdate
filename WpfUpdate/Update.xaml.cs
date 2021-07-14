using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using Update.Common;

namespace WpfUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update(string arg)
        {
            InitializeComponent();
            param.Text = arg;
            var paramter = arg.Split('|');

            var process = new Progress<HttpDownloadProgress>();
            HttpClient httpClient = new HttpClient();
            HttpClientExtensions.GetByteArrayAsync(httpClient, new Uri(paramter[3]), process);


            process.ProgressChanged += (obj, e) =>
            {
                //progress.Text =((int.Parse(e.BytesReceived.ToString())/int.Parse(e.TotalBytesToReceive.ToString()))*100).ToString();
                progress.Text = (double.Parse(e.BytesReceived.ToString())/ double.Parse(e.TotalBytesToReceive.ToString()) * 100).ToString("F2");
            };

        }

        private void OnDownloadStatistics(object sender, DownloadStatisticsEventArgs e)
        {
            progress.Text=$"下载速度：{e.Speed}，剩余时间：{e.Remaining.Minute}:{e.Remaining.Second}";
        }
    }
}
