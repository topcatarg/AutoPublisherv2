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
    public class Database
    {
        public static readonly string dbpath = Path.Combine(AppContext.BaseDirectory, "wpauto.db");

        public readonly int ActualVersion = 1;

        private int DatabaseVersion = -1;
        #region Constructor
        public Database()
        {

        }
        #endregion

        public async Task<bool> IsUpToDateAsync()
        {
            DatabaseConnection Con = new();
            using SqliteConnection db = Con.GetOpenDBConnection();
            DatabaseVersion = await db.QueryFirstOrDefaultAsync<int>(
@"select *
from version");
            return DatabaseVersion == ActualVersion;
        }

        /// <summary>
        /// Check if the file exists. If not, create it
        /// </summary>
        /// <returns>nothing</returns>
        public async Task CheckIfExistsDatabaseFileAsync()
        {
            if (!File.Exists(dbpath))
            {
                string DbBase = Path.Combine(AppContext.BaseDirectory, "base.db");
                using FileStream Source = File.Open(DbBase, FileMode.Open),
                    Destination = File.Create(dbpath);
                await Source.CopyToAsync(Destination);
            }
        }

        public async Task MigrateDatabaseAsync()
        {
            DatabaseConnection Conn = new();
            SqliteConnection db = Conn.GetOpenDBConnection();
            switch (DatabaseVersion)
            {
                case 0:
                    _ = await db.ExecuteAsync(
@"CREATE TABLE ""sites"" (
    ""id""    INTEGER,
	""URL""   TEXT NOT NULL,
	""User""  TEXT NOT NULL,
	""Pass""  TEXT NOT NULL,
	PRIMARY KEY(""id"" AUTOINCREMENT)
); 
update version set number = 1;");
                    goto case 1;
                case 1:
                    break;
            }
        }

    }
}
