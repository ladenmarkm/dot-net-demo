using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnicalTask.ViewModels
{
    public class Response
    {
        public int total_results { get; set; }
        //public string station_counts { get; set; }
        public string station_locator_url { get; set; }
        public List<FUEL_STATIONS> fuel_stations { get; set; }
    }
}