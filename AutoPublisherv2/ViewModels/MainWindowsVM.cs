using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoPublisherv2.Core;
using AutoPublisherv2.Helpers;

namespace AutoPublisherv2.ViewModels
{
    public class MainWindowsVM: INotifyMe
    {

        #region properties
        private string _footer = "";
        public string Footer
        {
            get => _footer;
            set
            {
                _footer = value;
                OnPropertyChanged();
            }
        }

        private Visibility _working = Visibility.Visible;
        public Visibility Working
        {
            get => _working;
            set
            {
                _working = value;
                OnPropertyChanged();
            }
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
