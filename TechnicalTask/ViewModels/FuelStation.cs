using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnicalTask.ViewModels
{
    public class fuel_station
    {
        public int Id { get; set; }

        [Display(Name ="Fuel Type Code")]
        public string fuel_type_code { get; set; }

        [Display(Name = "Station Name")]
        public string station_name { get; set; }

        [Display(Name = "Address")]
        public string street_address { get; set; }

        [Display(Name = "Directions")]
        public string intersection_directions { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "Zip")]
        public string zip { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }

        [Display(Name = "Phone Number")]
        public string station_phone { get; set; }

        [Display(Name = "Status")]
        public string status_code { get; set; }

        [Display(Name = "Access")]
        public string access_code { get; set; }        
    }
}