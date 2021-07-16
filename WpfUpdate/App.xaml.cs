using System;
using System.Windows;
using Update;
using Update.ViewModel;

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

            Update update = new Update(e.Args[0]);
            update.Show();

            //var param = e.Args[0].Split('|');
            //var vm = new UpdateVM(e.Args[0].Split('|'));
            //update.DataContext = vm;


            //Update2 test = new Update2(e.Args[0]);
            //test.Show();
        }
    }
}
