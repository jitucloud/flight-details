using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public string PNR { get; set; }
        public string PassengerName { get; set; }
        public string BookingDate { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalCityName { get; set; }       
        public string FlightNumber { get; set; }

        //  public City Departure { get; set; }
        //  public City Arrival { get; set; }
        //  public Passenger Passenger { get; set; }


    }
}