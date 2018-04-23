using Dapper.FluentMap.Mapping;
using FlightOperation.API.Model;

namespace FlightOperation.API.Map
{
    public class BookingMap : EntityMap<Booking>
    {
        public BookingMap()
        {
            Map(a => a.FlightNumber).ToColumn("flight_number", caseSensitive: false);
            Map(a => a.PNR).ToColumn("pnr", caseSensitive: false);
            Map(a => a.DepartureCityName).ToColumn("departure_city_name", caseSensitive: false);
            Map(a => a.ArrivalCityName).ToColumn("arrival_city_name", caseSensitive: false);
            Map(a => a.DepartureCityCode).ToColumn("departure_city_code", caseSensitive: false);
            Map(a => a.ArrivalCityCode).ToColumn("arrival_city_code", caseSensitive: false);
            Map(a => a.FirstName).ToColumn("firstname", caseSensitive: false);
            Map(a => a.LastName).ToColumn("lastname", caseSensitive: false);

        }
    }
}