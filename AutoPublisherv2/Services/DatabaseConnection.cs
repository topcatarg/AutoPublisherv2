using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoPublisherv2.Core;


namespace AutoPublisherv2.Services
{
    class DatabaseConnection
    {

        private readonly SqliteConnection db;
        private readonly string dbpath = Database.dbpath;
        public DatabaseConnection()
        {
            db = new SqliteConnection($"Data Source={dbpath}");
        }

        ~DatabaseConnection()
        {
            try
            {
                db.Close();
                db.Dispose();
            }
            catch
            {

            }
        }

        
        public SqliteConnection GetOpenDBConnection()
        {
            db.Open();
            return db;
        }
    }
}
