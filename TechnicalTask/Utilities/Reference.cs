using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechnicalTask.ViewModels;
using System.Data.Entity.Validation;
using System.Net;
using System.Linq;

namespace TechnicalTask.Utilities
{
    public static class Reference
    {
        // API key for data.gov
        public const string API_KEY = "S9Ukacr8eeuCsFFgxrlwydI4xf5etPA73O5UVrGQ";

        /// <summary>
        /// Grabs a list of the first 200 alternative fueling stations from NREL.GOV
        /// </summary>
        /// <returns>
        /// List of fueling stations
        /// </returns>
        public static List<FUEL_STATIONS> GetData()
        {
            var url = "https://developer.nrel.gov/api/alt-fuel-stations/v1.json?limit=200&api_key=" + API_KEY;
            var client = new WebClient();
            var jsonString = client.DownloadString(url);
            var items = JsonConvert.DeserializeObject<Response>(jsonString);
            var stations = items.fuel_stations;

            return stations;
        }

        /// <summary>
        /// Populates a database table with 200 fueling stations
        /// </summary>
        public static void PopulateDatabase()
        {
            using (var db = new DataContext())
            {
                // Clear all records from the table
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE FUEL_STATIONS");

                List<FUEL_STATIONS> stations = GetData();

                foreach (var station in stations)
                {
                    db.FUEL_STATIONS.Add(station);
                }

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public static List<fuel_station> GetViewModelList()
        {
            // Fuel type code, access code, country, status code
            var list = new List<fuel_station>();

            using (var db = new DataContext())
            {
                var q = from fs in db.FUEL_STATIONS
                        select fs;

                foreach (var p in q)
                {
                    var stationViewModel = new fuel_station();

                    stationViewModel.Id = p.Id;
                    stationViewModel.station_name = p.station_name;
                    stationViewModel.street_address = p.street_address;
                    stationViewModel.intersection_directions = p.intersection_directions;
                    stationViewModel.city = p.city;
                    stationViewModel.state = p.state;
                    stationViewModel.zip = p.zip;
                    stationViewModel.station_phone = p.station_phone;
                    
                    // Map fuel type codes to descriptive labels
                    switch (p.fuel_type_code)
                    {
                        case "BD":
                            stationViewModel.fuel_type_code = "Biodiesel (B20 and above)";
                            break;
                        case "CNG":
                            stationViewModel.fuel_type_code = "Compressed Natural Gas (CNG)";
                            break;
                        case "ELEC":
                            stationViewModel.fuel_type_code = "Electric";
                            break;
                        case "E85":
                            stationViewModel.fuel_type_code = "Ethanol (E85)";
                            break;
                        case "HY":
                            stationViewModel.fuel_type_code = "Hydrogen";
                            break;
                        case "LNG":
                            stationViewModel.fuel_type_code = "Liquefied Natural Gas (LNG)";
                            break;
                        case "LPG":
                            stationViewModel.fuel_type_code = "Propane (LPG)";
                            break;
                    }

                    // map oountry code to descriptive name
                    if (p.country == "US")
                    {
                        stationViewModel.country = "United States";
                    }
                    else if (p.country == "CA")
                    {
                        stationViewModel.country = "Canada";
                    }

                    // map access code to capitalized version
                    if (p.access_code == "public")
                    {
                        stationViewModel.access_code = "Public";
                    }
                    else if (p.access_code == "private")
                    {
                        stationViewModel.access_code = "Private";
                    }

                    // map status codes to descriptions
                    if (p.status_code == "E")
                    {
                        stationViewModel.status_code = "Available";
                    }
                    else if (p.status_code == "P")
                    {
                        stationViewModel.status_code = "Planned";
                    }
                    else if (p.status_code == "T")
                    {
                        stationViewModel.status_code = "Temporarily Unavailable";
                    }

                    list.Add(stationViewModel);
                }
            }

            return list;
        }
    }
}