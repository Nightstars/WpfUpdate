using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using WpfMainApp.Util;

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

            string currentVersion = "1.0.0";
            string lastVersion="1.0.1";
            string logHtml = "http://cloudapps.life:9003/";
            string updateUrl = "http://192.168.0.103:8080/Update.zip";
            string installPath = AppDomain.CurrentDomain.BaseDirectory;
            string updateZipMD5 = "1111";
            //Process.Start("Update.exe", $"{currentVersion}|{lastVersion}|{logHtml}|{updateUrl}|{installPath}|{updateZipMD5}");
            //Process.GetCurrentProcess().Kill();
        }
    }
}
