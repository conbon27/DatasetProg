using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonLD;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;

namespace DatasetProg
{
    class Program
    {
        static void Main(string[] args)
        {
            // creates a new URL object
            URL u = new URL();
            // connects to MySQL DB to read data
            List<StationData> results = u.ReadTable();
            // creates a new detail object
            Detail test1 = new Detail();
            // populates details & outputs to console before creating JSON-LD
            test1.ConfirmDetails(results);
            //test1.ListAntics();
            Console.ReadKey();
        }
    }
}