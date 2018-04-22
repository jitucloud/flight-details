using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Interface
{
    public interface IDbManager
    {
        /// <summary>
        /// Create and return a db connection, using the provided connection string details
        /// </summary>
        IDbConnection GetConnection();
        /// <summary>
        /// Create and return an open db connection, using the provided connection string details
        /// </summary>
        IDbConnection GetOpenConnection();
    }
}