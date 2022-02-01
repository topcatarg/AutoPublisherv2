using AutoPublisherv2.Helpers;
using AutoPublisherv2.Models;
using AutoPublisherv2.Services;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPublisherv2.ViewModels
{
    class ToolsVM: INotifyMe
    {

        #region properties
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

        private readonly SiteServices _siteServices;

        public bool ProgressControl
        { get; set; }
        #endregion

        private ToolsVM(SiteServices s, ImmutableList<Site> sites)
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

        ~ToolsVM()
        {
            _siteServices.SiteRefreshed -= RefreshState;
        }

        public async static Task<ToolsVM> ClassFactory()
        {

            SiteServices s = SiteServices.ClassFactory();
            ToolsVM toolsVM = new(s, await s.SiteList.Value);
            return toolsVM;
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

        public async Task CheckImages()
        {
            ProgressControl = true;
            string pwd = "Ebj(`h@552$2";
            string user = "ftpuser@topcatarg.com.ar";
            string host = "topcatarg.com.ar";

            Uri path = new("ftp://topcatarg.com.ar/topcatarg.com.ar/public_html/wp-content/uploads/");
            string path2 = "/topcatarg.com.ar/public_html/wp-content/uploads/";
            using FtpClient client = new(host, user, pwd);
            await client.ConnectAsync();
            //_ = await client.AutoConnectAsync();
            FtpListItem[] filelist = await client.GetListingAsync(path2);
            List<string> FullFileList = await AllFiles(filelist, client);
            
            ProgressControl = false;
        }
        private async Task<List<string>> AllFiles(FtpListItem[] Items, FtpClient client)
        {
            List<string> Files = new();
            foreach (FtpListItem file in Items)
            {
                if (file.Type == FtpFileSystemObjectType.File)
                {
                    Files.Add(file.FullName);
                }
                else if (file.Type == FtpFileSystemObjectType.Directory)
                {

                    FtpListItem[] filelist = await client.GetListingAsync(file.FullName);
                    List<string> NewFiles = await AllFiles(filelist, client);
                    Files.AddRange(NewFiles);
                }
            }
            return Files;
        }
    }

    
}
