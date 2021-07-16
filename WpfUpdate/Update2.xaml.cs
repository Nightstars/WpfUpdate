using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Update.Common;
using Update.Common.zip;
using Update.ViewModel;

namespace Update
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Update2 : Window
    {
        UpdateVM updateVM = new UpdateVM();
        string[] paramter = null;
        StringBuilder stringBuilder= new StringBuilder();
        public Update2(string arg)
        {
            InitializeComponent();
            paramter = arg.Split('|');
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            progress.Text = "正在连接服务器...";
            HttpUtil httpUtil = new HttpUtil();
            httpUtil.DownloadStatistics += Bootstrap_DownloadStatistics;
            httpUtil.DownloadFile(paramter[3], new FileInfo("./temp/Update.zip")).GetAwaiter();
        }

        private void Bootstrap_DownloadStatistics(object sender, DownloadStatisticsEventArgs e)
        {
            progress.Dispatcher.Invoke(() => progress.Text = "正在下载资源");
            pb_import.Dispatcher.Invoke(new Action<System.Windows.DependencyProperty, object>(pb_import.SetValue),
                                         DispatcherPriority.Background,
                                         ProgressBar.ValueProperty,
                                         double.Parse(e.Speed));

            txtJD.Dispatcher.Invoke(()=> txtJD.Text = $"当前进度：{e.Speed}%");

            if (e.Speed == "100")
            {
                progress.Dispatcher.Invoke(() => progress.Text = "正在解压资源");
                ZipUtil zipUtil = new ZipUtil();
                zipUtil.UnZipEvent += OnUnzip;
                var result = zipUtil.UnZipAsync("./temp/Update.zip", "./temp").GetAwaiter();
            }
        }

        public void OnUnzip(object sender, UnZipEventArgs e)
        {
            pb_import.Dispatcher.Invoke(() => pb_import.Maximum = e.Count);
            var x = ((double)e.Index / (double)e.Count) * 100;
            txtJD.Dispatcher.Invoke(() => txtJD.Text = $"当前进度：{x.ToString("F2")}%");

            pb_import.Dispatcher.Invoke(new Action<System.Windows.DependencyProperty, object>(pb_import.SetValue),
                                         DispatcherPriority.Background,
                                         ProgressBar.ValueProperty,
                                         double.Parse(e.Index.ToString()));
        }
    }
}
