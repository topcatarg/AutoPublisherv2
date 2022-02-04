using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoPublisherv2.Core;

namespace AutoPublisherv2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private async void ApplicationStart(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            //Check migration
            Database db = new();
            await db.CheckIfExistsDatabaseFileAsync();
            //There should be a personalized SplashScreen here, informing what is happening.
            if (!await db.IsUpToDateAsync())
            {
                await db.MigrateDatabaseAsync();
            }   
            //Start app
            MainWindow win = new();
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Current.MainWindow = win;
            win.Show();
        }
    }
}
