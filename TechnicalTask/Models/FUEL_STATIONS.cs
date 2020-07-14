namespace TechnicalTask
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FUEL_STATIONS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string fuel_type_code { get; set; }

        public string station_name { get; set; }

        public string street_address { get; set; }

        public string intersection_directions { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip { get; set; }

        public string country { get; set; }

        public string station_phone { get; set; }

        public string status_code { get; set; }

        public string access_code { get; set; }
    }
}
