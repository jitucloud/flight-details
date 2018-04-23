using Dapper.FluentMap.Mapping;
using FlightOperation.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Map
{
    /// <summary>
    /// Flight detail dapper mapping
    /// </summary>
    public class FlightDetailMap : EntityMap<FlightDetail>
    {
        public FlightDetailMap()
        {            
            Map(a => a.FlightNumber).ToColumn("flight_number", caseSensitive: false);
            Map(a => a.Id).ToColumn("flight_detail_id", caseSensitive: false);
            Map(a => a.DepartureDate).ToColumn("departure_date", caseSensitive: false);
            Map(a => a.ArrivalDate).ToColumn("arrival_date", caseSensitive: false);
            Map(a => a.DepartureTime).ToColumn("departure_time", caseSensitive: false);
            Map(a => a.ArrivalTime).ToColumn("arrival_time", caseSensitive: false);
            Map(a => a.Capacity).ToColumn("capacity", caseSensitive: false);
            Map(a => a.DepartureCityName).ToColumn("departure_city_name", caseSensitive: false);
            Map(a => a.ArrivalCityName).ToColumn("arrival_city_name", caseSensitive: false);
            Map(a => a.DepartureCityCode).ToColumn("departure_city_code", caseSensitive: false);
            Map(a => a.ArrivalCityCode).ToColumn("arrival_city_code", caseSensitive: false);
        }
    }
}