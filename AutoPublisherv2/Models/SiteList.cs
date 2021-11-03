using AutoPublisherv2.Helpers;
using System.Collections.ObjectModel;

namespace AutoPublisherv2.Models
{
    public class SiteList : INotifyMe
    {
        private bool ischecked;
        public bool IsChecked
        {
            get => ischecked;
            set
            {
                ischecked = value;
                OnPropertyChanged();
            }
        }

        private string siteurl;
        public string SiteUrl
        {
            get => siteurl;
            set
            {
                siteurl = value;
                OnPropertyChanged();
            }
        }

        private bool tested = false;
        public bool Tested
        {
            get => tested;
            set
            {
                tested = value;
                OnPropertyChanged();
            }
        }

        private bool state = false;
        public bool State
        {
            get => state;
            set

            {
                state = value;
                OnPropertyChanged();
            }
        }

        private int progressvalue = 0;
        public int ProgressValue
        {
            get => progressvalue;
            set
            {
                progressvalue = value;
                OnPropertyChanged();
            }
        }

        public string User { get; set; }

        public string Password { get; set; }

        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> categorylist = new();
        public ObservableCollection<Category> CategoryList
        {
            get => categorylist;
            set
            {
                categorylist = value;
                OnPropertyChanged();
            }
        }
    }
}
