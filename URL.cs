using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DatasetProg
{
    class URL
    {
        // creates connection variable
        private readonly MySqlConnection conn = new MySqlConnection("Server = danu6.it.nuigalway.ie; Database = mydb2463; Uid = mydb2463ca; Pwd = mi3tax");
        // creates reader
        MySqlDataReader reader = null;
        // create list of type StationData to store data from DB
        List<StationData> stationInfo = new List<StationData>();

       public List<StationData> ReadTable()
        {
            try
            {
                // open connection
                conn.Open();

                // read from table
                string readDB = @"SELECT * FROM Station;";
                MySqlCommand cmdRead = new MySqlCommand(readDB, conn);
                reader = cmdRead.ExecuteReader();

                // while reader is open
                while (reader.Read())
                {
                    // create new stationData object
                    StationData stationData = new StationData();
                    stationData.longitude = (string)reader["longitude"];
                    stationData.latitude = (string)reader["latitude"];
                    stationData.stationID = (string)reader["stationId"];
                    stationData.csv = (string)reader["csv"];
                    stationData.json = (string)reader["json"];

                    // write out columns to confirm data
                    Console.WriteLine(reader["longitude"] + " " + reader["latitude"] + " " + reader["stationID"]);
                    // adds stationData to list
                    stationInfo.Add(stationData);
                    
                }
            }
            catch (Exception ex)
            {
                // outputs error message
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // close the reader
                if (reader != null)
                {
                    reader.Close();
                }
                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
                //Console.ReadKey();
            }

            return stationInfo;
        }

        public List<StationData> GetList()
        {
            return stationInfo;
        }

    }
}
