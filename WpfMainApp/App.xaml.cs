using SmartSoft.common.Utils;
using SmartSoft.common.Utils.solution;
using System;
using System.Windows;


namespace WpfMainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new UpdateUtil().checckUpdate("./WpfMainApp.exe", "http://192.168.2.114:10002");
        }
    }
}
