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
    /// Lógica de interacción para PublishView.xaml
    /// </summary>
    public partial class PublishView : UserControl
    {

        private PublishVM _VM;
        public PublishView()
        {
            InitializeComponent();
        }

        private async void UserControl_Initialized(object sender, EventArgs e)
        {
            _VM = await PublishVM.ClassFactory();
            DataContext = _VM;

        }

        private void BtnCheckAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _VM.Sites)
            {
                item.IsChecked = true;
            }
        }

        private void BtnUncheckAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _VM.Sites)
            {
                item.IsChecked = false;
            }
        }

        private async void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            _VM.EnableButtons = false;
            List<Task> Tasks = new List<Task>();
            foreach (var v in _VM.Sites)
            {
                if (v.IsChecked)
                {
                    /*
                    Publisher_App_Pass p = new Publisher_App_Pass()
                    {
                        Connection = v,
                        Post = Post
                    };
                    Task t = Task.Run(async () => await (Task)p.Publish());
                    Tasks.Add(t);*/
                }
            }
            //await Task.WhenAll(Tasks.ToArray());
            _VM.EnableButtons = true;
        }

        private async void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            _VM.EnableButtons = false;
            List<Task> Tasks = new List<Task>();
            foreach (var v in _VM.Sites)
            {
                if (v.IsChecked)
                {
                    /*
                    //Publisher p = new Publisher()
                    Publisher_App_Pass p = new Publisher_App_Pass()
                    {
                        Connection = v
                    };
                    Task t = Task.Run(async () => await (Task)p.TestConnection());
                    Tasks.Add(t);
                    */
                }
            }
            //await Task.WhenAll(Tasks.ToArray());
            _VM.EnableButtons = true;
        }

        private async void BtnCategorias_Click(object sender, RoutedEventArgs e)
        {
            _VM.EnableButtons = false;
            List<Task> Tasks = new List<Task>();
            List<Tuple<Models.SiteList, List<WordPressPCL.Models.Category>>> UpdateList = new List<Tuple<Models.SiteList, List<WordPressPCL.Models.Category>>>();
            foreach (Models.SiteList v in _VM.Sites)
            {
                v.CategoryList.Clear();
                if (v.IsChecked)
                {
                    /*
                    SiteData sd = new SiteData() { Connection = v };
                    Task t = Task.Run(async () =>
                    {
                        var list = await sd.GetCategories();
                        UpdateList.Add(new Tuple<Models.SiteList, List<WordPressPCL.Models.Category>>(v, list));
                    });
                    Tasks.Add(t);
                    */
                }
            }
            await Task.WhenAll(Tasks.ToArray());
            foreach (var item in UpdateList)
            {
                foreach (var cat in item.Item2)
                {
                    item.Item1.CategoryList.Add(new Models.Category()
                    {
                        Id = cat.Id,
                        Name = cat.Name
                    });
                }
            }
            _VM.EnableButtons = true;
        }


    }
}
