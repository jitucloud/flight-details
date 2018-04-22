using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    public class SearchBookingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string PNR { get; set; }

    }
}