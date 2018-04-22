using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightOperation.API.Model
{
    public class City
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}