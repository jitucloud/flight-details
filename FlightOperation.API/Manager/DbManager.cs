using FlightOperation.API.Helper;
using FlightOperation.API.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Manager
{
    public class DbManager : IDbManager
    {
        private ConnectionStringSettings _connectionString;

        public DbManager(string dbname)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[dbname];
        }

        public IDbConnection GetConnection()
        {
            var factory = DbProviderFactories.GetFactory(_connectionString.ProviderName);
            var conn = factory.CreateConnection();
            conn.ConnectionString = _connectionString.ConnectionString;
            return conn;
        }

        public IDbConnection GetOpenConnection()
        {
            var conn = GetConnection();
            conn.Open();
            return conn;
        }
    }
}