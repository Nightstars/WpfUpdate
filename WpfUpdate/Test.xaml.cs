using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Update.Common;
using Update.ViewModel;

namespace Update
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        UpdateVM updateVM = new UpdateVM();
        public Test(string arg)
        {
            InitializeComponent();
            param.Text = arg;
            var paramter = arg.Split('|');
            container.DataContext = updateVM;

            var process = new Progress<HttpDownloadProgress>();
            HttpClient httpClient = new HttpClient();
            HttpClientExtensions.GetByteArrayAsync(httpClient, new Uri(paramter[3]), process);

            process.ProgressChanged += (obj, e) =>
            {
                //progress.Text = ((int.Parse(e.BytesReceived.ToString()) / int.Parse(e.TotalBytesToReceive.ToString())) * 100).ToString();
                //progressBar.Value = (double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100);
                Debug.WriteLine((double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100));

                int x = int.Parse(Math.Round(double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100,0).ToString());

                    updateVM.ProgressValue = (double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100);

            };
            //progress.Text = $"下载速度：0，剩余时间：xxx";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            while (updateVM._progressValuelue < 100)
            {
                updateVM.ProgressValue++;
            }
        }
    }
}
