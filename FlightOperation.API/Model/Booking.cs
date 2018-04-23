using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    /// <summary>
    /// Booking class
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }
        public string PNR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookingDate { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalCityName { get; set; }
        public string DepartureCityCode { get; set; }
        public string ArrivalCityCode { get; set; }
        public string FlightNumber { get; set; }
    }
}