using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length==0 || e.Args[0].Split('|').Length != 6)
            {
                Environment.Exit(0);
            }
            Update updater = new Update(e.Args[0]);
            //var param = e.Args[0].Split('|');
            //var vm = new UpdateVM(param, updater.Close);
            updater.Show();
        }
    }
}
