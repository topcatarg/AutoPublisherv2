using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoPublisherv2.Helpers;
using AutoPublisherv2.Models;
using AutoPublisherv2.Services;
using Microsoft.Data.Sqlite;

namespace AutoPublisherv2.ViewModels
{
    internal class ConfigVM: INotifyMe
    {
        #region Properties
        private Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                _site = value;
                OnPropertyChanged();
                OnPropertyChanged("CanEdit");
            }
        }

        private ObservableCollection<Site> _sites = new();
        public ObservableCollection<Site> Sites
        {
            get => _sites;
            set
            {
                _sites = value;
                OnPropertyChanged();
            }
        }

        private bool _editing;
        public bool Editing
        {
            get => _editing;
            set
            {
                _editing = value;
                OnPropertyChanged();
            }
        }

        public bool Adding { get; set; }
        public Site OldSite { get; set; }

       

        private readonly SiteServices _siteServices;

        #endregion

        private ConfigVM(ImmutableList<Site> sites, SiteServices siteServices)
        {
            Sites = new ObservableCollection<Site>(sites);
            _siteServices = siteServices;
            _siteServices.SiteRefreshed += RefreshState;
        }

        ~ConfigVM()
        {
            _siteServices.SiteRefreshed -= RefreshState;
        }

        public async static Task<ConfigVM> ClassFactory()
        {

            SiteServices s = SiteServices.ClassFactory();
            ConfigVM vm = new(await s.SiteList.Value, s);
            return vm;
        }

        public async Task Upsert()
        {
            SiteServices s = SiteServices.ClassFactory();
            if (Site.Id == 0)
            {
                Sites.Add(await s.InsertSite(Site));
            }
            else
            {
                _ = await s.UpdateSite(Site);
            }
        }

        public async Task Delete()
        {
            SiteServices s = SiteServices.ClassFactory();
            bool state = await s.DeleteSite(Site);
            if (state)
            {
                _ = Sites.Remove(Site);
            }
        }

        public async void RefreshState()
        {
            Sites = new ObservableCollection<Site>(await _siteServices.SiteList.Value);
        }

    }
}
