using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Net;
using System.IO;
using FluentFTP;
using AutoPublisherv2.ViewModels;

namespace AutoPublisherv2.Views
{
    /// <summary>
    /// Lógica de interacción para Tools.xaml
    /// </summary>
    public partial class Tools : UserControl
    {

        private ToolsVM _VM;
        public Tools()
        {
            InitializeComponent();
        }

        private async void BtnCleanImages_Click(object sender, RoutedEventArgs e)
        {

            await _VM.CheckImages();
            /*
            //Uri path = new("ftp://topcatarg.com.ar/");
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(user,pwd);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string resul = reader.ReadToEnd();

            //Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();

            /*
            var connectionInfo = new ConnectionInfo("topcatarg.com.ar",
                                        21,
                                        user,
                                        new PasswordAuthenticationMethod(user, pwd));
            
            using SftpClient client = new SftpClient(connectionInfo);
            client.Connect();
            client.ChangeDirectory("/topcatarg.com.ar/public_html/wp-content/uploads");
            List<Renci.SshNet.Sftp.SftpFile> files = client.ListDirectory("/topcatarg.com.ar/public_html/wp-content/uploads").ToList();
            */
        }

        private async void UserControl_Initialized(object sender, EventArgs e)
        {
            _VM = await ToolsVM.ClassFactory();
            DataContext = _VM;
        }
    }
}
