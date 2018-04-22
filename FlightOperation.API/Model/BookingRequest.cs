using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    public class CreateBookingRequestDeatils
    {
        public FlightDetailRequest FlightDetails { get; set; }
        public List<Passenger> Passenger { get; set; }
    }

    public class FlightDetailRequest
    {
        public string DepartureCityCode { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalCityCode { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
    }

    public class BookingRequestWrapper
    {
        public List<CreateBookingRequestDeatils> bookingDetails { get; set; }
        public BookingRequestWrapper()
        {
            bookingDetails = new List<CreateBookingRequestDeatils>();
        }
    }
}
