using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    /// <summary>
    /// Flight Details class
    /// </summary>
    public class FlightDetail
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        private DateTime? departure_date { get; set; }

        public string DepartureDate
        {
            get
            {
                if (departure_date.HasValue)
                    return departure_date.Value.ToShortDateString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    departure_date = DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Utc);
                else
                    departure_date = null;
            }
        }
        public TimeSpan DepartureTime { get; set; }
        private DateTime? arrival_date { get; set; }

        public string ArrivalDate
        {
            get
            {
                if (arrival_date.HasValue)
                    return arrival_date.Value.ToShortDateString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    arrival_date = DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Utc);
                else
                    arrival_date = null;
            }
        }
        public TimeSpan ArrivalTime { get; set; }
        public int Capacity { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalCityName { get; set; }
        public string DepartureCityCode { get; set; }
        public string ArrivalCityCode { get; set; }

    }
}