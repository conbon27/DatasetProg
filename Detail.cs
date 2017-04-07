using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DatasetProg
{
    internal class Detail
    {
        public void ConfirmDetails(List<StationData> results)
        {
            // creates new Url object
            var u = new Url();
            // 'results' stores data taken from external DB
            results = u.ReadTable();

            for (var i = 0; i < 33; i++)
            {
                // populate info with default values for Json-ld files
                // using object initiator to create, instantiate & add attributes to each object
                var contact = new ContactPoint
                {
                    type = "ContactPoint",
                    contactType = "customer service",
                    telephone = "+353 9 138 7200",
                    email = "institute.mail@marine.ie"
                };

                var creator = new Creator
                {
                    type = "Organization",
                    url = "http://www.marine.ie/Home/",
                    name =
                        "Marine Institute, State agency responsible for marine research, technology development and innovation in Ireland",
                    contactPoint = contact
                };

                var inc = new IncludedInDataCatalog
                {
                    type = "DataCatalog",
                    name = "http://data.marine.ie/Category/Index/12"
                };

                // distribution info used to get direct file access
                var dist1 = new Distribution
                {
                    type = "DataDownload",
                    encodingFormat = "JSON",
                    contentUrl = results[i].Json
                };

                // e.g. "http://erddap.marine.ie/erddap/tabledap/IMI-TidePrediction.json?stationID,time,Water_Level,Water_Level_ODM,longitude,latitude&stationID=%22Achill_Island_MODELLED%22&time%3E=2017-01-01T00:00:00Z&time<=2020-01-01T00:20:00Z&longitude%3E=-10.27019&longitude<=-5.92167&latitude%3E=51.53115&latitude<=55.37168"; // get from DB


                var dist2 = new Distribution
                {
                    type = "DataDownload",
                    encodingFormat = "CSV",
                    contentUrl = results[i].Csv
                };
                // taken from DB

                var g = new Geo
                {
                    type = "GeoCoordinates",
                    longitude = results[i].Longitude,
                    latitude = results[i].Latitude
                };
                // e.g. "53.95219" & "-10.101596" - taken from DB

                var spat = new SpatialCoverage
                {
                    type = "Place",
                    geo = g
                };

                // object containing references to other JSON objects
                var root = new Dataset
                {
                    context = "http://schema.org/",
                    type = "Dataset",
                    name = "Marine Institute Tide Prediction",
                    description =
                        "Tidal predictions are generated from measured data via the Irish National Tide Gauge Network ...",
                    url = "http://data.marine.ie/Dataset/Details/20955",
                    sameAs = "http://data.marine.ie/Category/Index/12",
                    keywords = new List<string>
                    {
                        "OCEANOGRAPHY > TIDE > TIDAL WATER",
                        "OCEANOGRAPHY > TIDE > PREDICTIONS",
                        "OCEANOGRAPHY > TIDE > " + results[i].StationId
                    },
                    includedInDataCatalog = inc,
                    creator = creator,
                    distribution = new List<Distribution> {dist1, dist2},
                    temporalCoverage = "2017-01-01/2020-01-01",
                    spatialCoverage = spat
                };
                // keywords to be updated by method - pull station_id from DB to be entered in keywords List 
                // important for rest service as used to perform index search in PHP

                // visual check shows data taken-in from DB
                Console.WriteLine("Details Confirmed: " + g.longitude + " " + g.latitude);

                // creation of List of type dataset with all the above info added using the Collections initializer
                var oj = new List<Dataset> {root};

                // added above set values in list to rendered JSON file - with indent formatting.
                var json = JsonConvert.SerializeObject(oj, Formatting.Indented);
                // as a check of the format - immediate output to console for viewing
                Console.WriteLine(json);
                // escape point here if format/output incorrect during testing
                // Console.ReadKey();
                // serialize JSON to a string and then write string to a file
                File.WriteAllText(@"C:\Users\aconw\Downloads\JSONDirectory1\Dataset_" + results[i].StationId + ".jsonld",
                    JsonConvert.SerializeObject(oj, Formatting.Indented));
            }
        }
    }
}