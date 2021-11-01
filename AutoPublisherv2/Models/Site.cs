using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoPublisherv2.Helpers;

namespace AutoPublisherv2.Models
{
    internal class Site:INotifyMe
    {
        private int _id;
        private string _siteurl;
        private string _user;
        private string _password;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string SiteURL
        {
            get => _siteurl;
            set
            {
                _siteurl = value;
                OnPropertyChanged();
            }
        }
        public string User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }
}
