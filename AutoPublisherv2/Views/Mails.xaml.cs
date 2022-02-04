using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAGetMail;

namespace AutoPublisherv2.Views
{
    /// <summary>
    /// Lógica de interacción para Mails.xaml
    /// </summary>
    public partial class Mails : UserControl
    {
        public Mails()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MailServer oServer = new("outlook.office365.com",
                        "topcat_arg@hotmail.com",
                        "gonzahotmail1976",
                        ServerProtocol.Imap4);
            oServer.SSLConnection = true;
            oServer.Port = 993;
            MailClient oClient = new("TryIt");
            oClient.Connect(oServer);
            var folders = oClient.GetFolders();
            MailInfo[] infos = oClient.GetMailInfos();
            

        }
    }
}
