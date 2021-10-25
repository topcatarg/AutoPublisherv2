using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoPublisherv2.Services;

namespace AutoPublisherv2.Core
{
    class Database
    {
        public static readonly string dbpath = Path.Combine(AppContext.BaseDirectory, "wpauto.db");

        #region Constructor
        public Database()
        {

        }
        #endregion

        public async Task<string> CheckDatabase()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            await CheckIfExistsAsync();
            using SqliteConnection db = dbConnection.GetOpenDBConnection();
            int version = await db.QueryFirstOrDefaultAsync<int>(
@"select *
from version");
            if (version == 0)
            {
                _ = await db.ExecuteAsync(
@"CREATE TABLE ""sites"" (
    ""id""    INTEGER,
	""URL""   TEXT NOT NULL,
	""User""  TEXT NOT NULL,
	""Pass""  TEXT NOT NULL,
	PRIMARY KEY(""id"" AUTOINCREMENT)
); 
update version set number = 1;");
                return "base de datos elevada a version 1";
            }
            else
            {
                return "base de datos en version 1";
            }
        }

        private async Task CheckIfExistsAsync()
        {
            if (!File.Exists(dbpath))
            {
                string DbBase = Path.Combine(AppContext.BaseDirectory, "base.db");
                using FileStream Source = File.Open(DbBase, FileMode.Open),
                    Destination = File.Create(dbpath);
                await Source.CopyToAsync(Destination);
            }
        }
    }
}
