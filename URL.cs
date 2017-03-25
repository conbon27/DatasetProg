using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatasetProg
{
    internal class Url
    {
        // creates connection variable
        private readonly MySqlConnection _conn = new MySqlConnection("Server = danu6.it.nuigalway.ie; Database = mydb2463; Uid = mydb2463ca; Pwd = mi3tax");
        // creates reader
        private MySqlDataReader _reader = null;
        // create private read only list of type StationData to store data from DB
        private readonly List<StationData> _stationInfo = new List<StationData>();

       public List<StationData> ReadTable()
        {
            try
            {
                // open connection
                _conn.Open();

                // read from table with constant string variable
                const string readDb = @"SELECT * FROM Station;";
                var cmdRead = new MySqlCommand(readDb, _conn);
                _reader = cmdRead.ExecuteReader();

                // while reader is open
                while (_reader.Read())
                {
                    // create new stationData object using the object initializer
                    var stationData = new StationData
                    {
                        Longitude = (string) _reader["longitude"],
                        Latitude = (string) _reader["latitude"],
                        StationId = (string) _reader["stationId"],
                        Csv = (string) _reader["csv"],
                        Json = (string) _reader["json"]
                    };

                    // write out columns to confirm data
                    Console.WriteLine(_reader["longitude"] + " " + _reader["latitude"] + " " + _reader["stationID"]);
                    // adds stationData to list
                    // see Nunit test
                    _stationInfo.Add(stationData);
                }
            }
            catch (Exception ex)
            {
                // outputs error message
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // close the reader with null propogation
                _reader?.Close();
                // close the connection with null propogation
                _conn?.Close();
                // used as an escape method when testing method function
                //Console.ReadKey();
            }

            return _stationInfo;
        }
        // method to return _stationInfo List collection for testing
        public List<StationData> GetList()
        {
            return _stationInfo;
        }

    }
}
