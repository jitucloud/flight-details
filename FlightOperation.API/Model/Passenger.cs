using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}