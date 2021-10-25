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
using MahApps.Metro.Controls;
using AutoPublisherv2.ViewModels;

namespace AutoPublisherv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainWindowsVM ThisVM = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ThisVM;
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await ThisVM.CheckDatabase();
            ThisVM.Enabled = true;
            ThisVM.Working = Visibility.Hidden;
        }
    }
}
