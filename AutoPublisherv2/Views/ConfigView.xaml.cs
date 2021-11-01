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
using AutoPublisherv2.ViewModels;

namespace AutoPublisherv2.Views
{
    /// <summary>
    /// Lógica de interacción para ConfigView.xaml
    /// </summary>
    public partial class ConfigView : UserControl
    {

        ConfigVM VM;
        public ConfigView()
        {
            InitializeComponent();
        }

        async private void UserControl_Initialized(object sender, EventArgs e)
        {
            VM = await ConfigVM.ClassFactory();
            DataContext = VM;
            if (dgSites.Items.Count > 0)
            {
                dgSites.SelectedIndex = 0;
            }
            ProgressControl.Visibility = Visibility.Hidden;
            ChangeView(false);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ChangeView(true);
            VM.OldSite = VM.Site;
            VM.Site = new Models.Site();
            VM.Adding = true;
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (VM.Site == null)
            {
                return;
            }
            ProgressControl.Visibility = Visibility.Visible;
            await VM.Delete();
            dgSites.SelectedIndex = dgSites.Items.Count - 1;
            //await PublishView.RefreshView();
            ProgressControl.Visibility = Visibility.Hidden;
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            //Must add
            ProgressControl.Visibility = Visibility.Visible;
            await VM.Upsert();
            if (VM.Adding)
                dgSites.SelectedIndex = dgSites.Items.Count - 1;
            VM.Adding = false;
            ProgressControl.Visibility = Visibility.Hidden;
            ChangeView(false);
            //await PublishView.RefreshView();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (VM.Site == null)
            {
                return;
            }

            VM.OldSite = new Models.Site
            {
                Id = VM.Site.Id,
                SiteURL = VM.Site.SiteURL,
                Password = VM.Site.Password,
                User = VM.Site.User
            };
            ChangeView(true);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            VM.Adding = false;
            ChangeView(false);
            VM.Site.Id = VM.OldSite.Id;
            VM.Site.User = VM.OldSite.User;
            VM.Site.SiteURL = VM.OldSite.SiteURL;
            VM.Site.Password = VM.OldSite.Password;
        }

        private void ChangeView(bool Editing)
        {
            dgSites.IsEnabled = !Editing;
            VM.Editing = Editing;
            Visibility ButtonsEditing;
            Visibility ButtonsAdding;
            if (Editing)
            {
                ButtonsEditing = Visibility.Visible;
                ButtonsAdding = Visibility.Hidden;
            }
            else
            {
                ButtonsEditing = Visibility.Hidden;
                ButtonsAdding = Visibility.Visible;
            }
            BtnAdd.Visibility = ButtonsAdding;
            BtnEdit.Visibility = ButtonsAdding;
            BtnDelete.Visibility = ButtonsAdding;
            BtnOk.Visibility = ButtonsEditing;
            BtnCancel.Visibility = ButtonsEditing;
        }
    }
}
