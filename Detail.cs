using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetProg
{
    class Detail
    {

        public void ConfirmDetails(List<StationData> results)
        {
           URL u = new URL();
           // results stores data taken from external DB
           results = u.ReadTable();

            for (int i = 0; i < 33; i++)
            {
                // populate info with default values for Json-ld files
                ContactPoint contact = new ContactPoint();
                contact.type = "ContactPoint";
                contact.contactType = "customer service";
                contact.telephone = "+353 9 138 7200";
                contact.email = "institute.mail@marine.ie";

                Creator creator = new Creator();
                creator.type = "Organization";
                creator.url = "http://www.marine.ie/Home/";
                creator.name = "Marine Institute, State agency responsible for marine research, technology development and innovation in Ireland";
                creator.contactPoint = contact;

                IncludedInDataCatalog inc = new IncludedInDataCatalog();
                inc.type = "DataCatalog";
                inc.name = "http://data.marine.ie/Category/Index/12";

                // need to be adjusted as per the rest service path - method to be created
                Distribution dist1 = new Distribution();
                dist1.type = "DataDownload";
                dist1.encodingFormat = "JSON";
                dist1.contentUrl = results[i].json; // e.g. "http://erddap.marine.ie/erddap/tabledap/IMI-TidePrediction.json?stationID,time,Water_Level,Water_Level_ODM,longitude,latitude&stationID=%22Achill_Island_MODELLED%22&time%3E=2017-01-01T00:00:00Z&time<=2020-01-01T00:20:00Z&longitude%3E=-10.27019&longitude<=-5.92167&latitude%3E=51.53115&latitude<=55.37168"; // get from DB


                Distribution dist2 = new Distribution();
                dist2.type = "DataDownload";
                dist2.encodingFormat = "CSV";
                dist2.contentUrl = results[i].csv; // taken from DB

                Geo g = new Geo();
                g.type = "GeoCoordinates";
                g.longitude = results[i].longitude; // e.g. "53.95219" - taken from DB
                g.latitude = results[i].latitude; // taken from DB

                SpatialCoverage spat = new SpatialCoverage();
                spat.type = "Place";
                spat.geo = g;

                // object containing references to other JSON objects
                Dataset root = new Dataset();
                root.context = "http://schema.org/";
                root.type = "Dataset";
                root.name = "Marine Institute Tide Prediction";
                root.description = "Tidal predictions are generated from measured data via the Irish National Tide Gauge Network ...";
                root.url = "http://data.marine.ie/Dataset/Details/20955";
                root.sameAs = "http://data.marine.ie/Category/Index/12";
                // keywords to be updated by method - pull station_id from DB
                root.keywords = new List<string>() {"OCEANOGRAPHY > TIDE > TIDAL WATER",
                "OCEANOGRAPHY > TIDE > PREDICTIONS",
                "OCEANOGRAPHY > TIDE > "+ results[i].stationID};
                root.includedInDataCatalog = inc;
                root.creator = creator;
                root.distribution = new List<Distribution>() { dist1, dist2 };
                // important for rest service if used
                root.temporalCoverage = "2017-01-01/2020-01-01";
                root.spatialCoverage = spat;

                // check shows data transfered in form DB
                Console.WriteLine("Details Confirmed: " + g.longitude + " " + g.latitude);

                // creation of List of type dataset with all the above info added
                var oj = new List<Dataset>();
                oj.Add(root);

                // added above set values in list to rendered JSON file - formatted.
                string json = JsonConvert.SerializeObject(oj, Newtonsoft.Json.Formatting.Indented);
                // as a check of the format - immediate output for viewing
                Console.WriteLine(json);
                // escape point here if format/output incorrect
                // Console.ReadKey();
                // serialize JSON to a string and then write string to a file
                File.WriteAllText(@"C:\Users\aconw\Downloads\JSONDirectory\Dataset"+i+".jsonld", JsonConvert.SerializeObject(oj, Newtonsoft.Json.Formatting.Indented));
            }
        }

    }
}
