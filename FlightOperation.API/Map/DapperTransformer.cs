using Dapper.FluentMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Map
{
    /// <summary>
    /// Main dapper transformer class
    /// </summary>
    public static class DapperTransformer
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new FlightDetailMap());
                config.AddMap(new BookingMap());

            });
        }
    }
}