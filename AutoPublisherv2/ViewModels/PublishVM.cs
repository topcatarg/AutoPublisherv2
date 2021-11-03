using AutoPublisherv2.Helpers;
using AutoPublisherv2.Models;
using AutoPublisherv2.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPublisherv2.ViewModels
{
    internal class PublishVM: INotifyMe
    {
        #region Properties

        private SiteList site;
        public SiteList Site
        {
            get => site;
            set
            {
                site = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SiteList> sites = new ObservableCollection<SiteList>();
        public ObservableCollection<SiteList> Sites
        {
            get => sites;
            set
            {
                sites = value;
                OnPropertyChanged();
            }
        }

        public bool ProgressControl
        { get; set; }

        private bool enableButtons = true;
        public bool EnableButtons
        {
            get => enableButtons;
            set
            {
                enableButtons = value;
                OnPropertyChanged();
                ProgressControl = !enableButtons;
            }
        }
        private readonly SiteServices _siteServices;
        #endregion
        private PublishVM(SiteServices s, ImmutableList<Site> sites)
        {
            _siteServices = s;
            _siteServices.SiteRefreshed += RefreshState;
            Sites = new ObservableCollection<SiteList>(sites.Select(
                x => new SiteList()
                {
                    Password = x.Password,
                    SiteUrl = x.SiteURL,
                    User = x.User
                }));
        }

        ~PublishVM()
        {
            _siteServices.SiteRefreshed -= RefreshState;
        }

        public async static Task<PublishVM> ClassFactory()
        {

            SiteServices s = SiteServices.ClassFactory();
            PublishVM publishVM = new(s, await s.SiteList.Value);
            return publishVM;
        }

        public async void RefreshState()
        {
            ImmutableList<Site> v = await _siteServices.SiteList.Value;
            Sites = new ObservableCollection<SiteList>(v.Select(x => new SiteList()
            {
                Password = x.Password,
                SiteUrl = x.SiteURL,
                User = x.User
            }));
        }
    }
}
