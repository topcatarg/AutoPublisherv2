using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoPublisherv2.Services;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.ObjectModel;
using AutoPublisherv2.Models;
using System.Collections.Immutable;

namespace AutoPublisherv2.Services
{
    internal class SiteServices
    {
        public Action SiteRefreshed;
        private static SiteServices ThisInstance;
        public Lazy<Task<ImmutableList<Site>>> SiteList;
        
        private SiteServices()
        {
            AssingSiteList();
        }

        private void AssingSiteList()
        {
            SiteList = new Lazy<Task<ImmutableList<Site>>>(
                            async () => await GetAllSites());
        }

        private void RefreshState()
        {
            SiteRefreshed?.Invoke();
            AssingSiteList();
        }

        private async Task<ImmutableList<Site>> GetAllSites()
        {
            DatabaseConnection db = new();
            using SqliteConnection conn = db.GetOpenDBConnection();
            IEnumerable<Site> result = await conn.QueryAsync<Site>(
@"select 
id as Id,
URL as SiteURL,
User,
Pass as Password
from sites");
            return result.ToImmutableList<Site>();
        }
        public static SiteServices ClassFactory()
        {
            if (ThisInstance == null)
            {
                ThisInstance = new();
            }
            return ThisInstance;
        }
        public async Task<ObservableCollection<Site>> GetSiteList()
        {
            DatabaseConnection db = new();
            using SqliteConnection conn = db.GetOpenDBConnection();
            IEnumerable<Site> result = await conn.QueryAsync<Site>(
@"select 
id as Id,
URL as SiteURL,
User,
Pass as Password
from sites");
            return new ObservableCollection<Site>(result);
        }

        public async Task<Site> InsertSite(Site site)
        {
            DatabaseConnection db = new();
            using SqliteConnection conn = db.GetOpenDBConnection();
            string Query = @"
insert into sites(Url,User,Pass)
values (@SiteURL,@User,@Password);

select 
id as Id,
URL as SiteURL,
User,
Pass as Password
from sites
where id=
(select max(id) from sites);";
            Site result = await conn.QueryFirstAsync<Site>(Query, new
            {
                site.SiteURL,
                site.User,
                site.Password
            });
            RefreshState();
            return result;
        }

        /// <summary>
        /// Update a site
        /// </summary>
        /// <param name="site">The site object with the new data</param>
        /// <returns>True if the site was updated ok. False in any other case</returns>
        public async Task<bool> UpdateSite(Site site)
        {
            DatabaseConnection db = new();
            using SqliteConnection conn = db.GetOpenDBConnection();
            string Query = @"
Update sites 
set Url=@Url, User=@User,Pass=@Pass 
where id=@Id;";
            int rows = await conn.ExecuteAsync(Query, new
            {
                @Url = site.SiteURL,
                @User = site.User,
                @Pass = site.Password,
                @Id = site.Id
            });
            RefreshState();
            return rows > 0;
        }

        /// <summary>
        /// Delete a site
        /// </summary>
        /// <param name="site">Object site to be deleted</param>
        /// <returns>True if the site was deleted ok. False in any other case</returns>
        public async Task<bool> DeleteSite(Site site)
        {
            DatabaseConnection db = new();
            using SqliteConnection conn = db.GetOpenDBConnection();
            string query =
@"delete
from sites
where id = @id;";
            int rows = await conn.ExecuteAsync(query, new
            {
                @id = site.Id
            });
            RefreshState();
            return rows > 0;
        }
    }
}
