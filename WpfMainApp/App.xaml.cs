﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            string currentVersion = "1.0.0";
            string lastVersion="1.0.1";
            string logHtml = "http://cloudapps.life:9003/";
            string updateUrl = "http://192.168.216.1/Update.zip";
            string installPath = AppDomain.CurrentDomain.BaseDirectory;
            string updateZipMD5 = "1111";
            Process.Start("Update.exe", $"{currentVersion}|{lastVersion}|{logHtml}|{updateUrl}|{installPath}|{updateZipMD5}");
        }
    }
}
