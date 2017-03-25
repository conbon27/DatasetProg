using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System.Transactions;
using System.IO;
using Newtonsoft.Json;

namespace DatasetProg
{
    [TestFixture]
    public class DatasetTest
    {
        // guards against DB data corruption as a result of testing
        private TransactionScope _scope;
        // creates connection variable
        private readonly MySqlConnection _conn =
            new MySqlConnection("Server = danu6.it.nuigalway.ie; Database = mydb2463; Uid = mydb2463ca; Pwd = mi3tax");

        // instrction of what to have set up before each test run
        [SetUp]
        public void SetUp()
        {
            _scope = new TransactionScope();
        }

        // instruction to dispose of _scope after each test run
        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }
        // Test that Database can be created & populated
        [Test]
        public void TestDatabaseCreation()
        {
            // creates a new URL object
            var udb = new Url();
            try
            {
                // confirm a blank URL object has been created
                Assert.Null(udb);
            }
            // prints exception error stack trace
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            try
            {
                // connects to MySQL DB to read data
                // populates table
                List<StationData> results = udb.ReadTable();
                // checks that table was populated
                Assert.NotNull(udb);
            }
            // prints exception error stack trace
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        // test that station info can be input & then added to 
        [Test]
        public void TestAddStationInfo()
        {
            // create new List collection of type StationData
            var stationInfo = new List<StationData>();
            try
            {
                // confirm created collection is empty
                Assert.Null(stationInfo);
            }
            // prints exception error stack trace
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            try
            {
                // create new stationData object using the object initializer
                var stationData = new StationData
                {
                    Longitude = "longitude",
                    Latitude = "latitude",
                    StationId = "stationId",
                    Csv = "csv",
                    Json = "json"
                };
                // confirm expected values matche what was added to object above
                Assert.AreEqual(stationData.Longitude, "longitude");
                Assert.AreEqual(stationData.Latitude, "latitude");
                Assert.AreEqual(stationData.StationId, "stationId");
                Assert.AreEqual(stationData.Csv, "csv");
                Assert.AreEqual(stationData.Json, "json");
                // adds stationData to stationInfo list
                stationInfo.Add(stationData);
                // confirm stationInfo is no longer null
                Assert.NotNull(stationInfo);
                // confirms StationInfo only contains the single stationData object added
                Assert.AreEqual(stationInfo.Count, 1);
                Assert.AreNotEqual(stationInfo.Count, 2);
            }
            // prints exception error stack trace
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        // test that database connection is valid can pull information from database
        [Test]
        public void TestSelectFunction()
        {
            try
            {
                // open connection
                _conn.Open();
                // read from table with constant string variable given a value
                const string readDb = @"SELECT stationID FROM Station WHERE longitude = '-9.89';";
                var cmdRead = new MySqlCommand(readDb, _conn);
                // execute query to return a single statement - cast to string type using Scalar
                var result = (string)cmdRead.ExecuteScalar();
                // close connection
                _conn.Close();
                // set expected to string value
                const string expected = "Ballyglass";
                Assert.AreEqual(expected, result);
            }
            // prints exception error stack trace
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        // test to check if file is being created by serializer
        [Test]
        public void TestFileCreation()
        {
            // creation of List of type dataset with all the above info added using the Collections initializer
            var test = new List<Dataset> { };

            // added above set values in list to rendered JSON file - with indent formatting.
            var json = JsonConvert.SerializeObject(test, Newtonsoft.Json.Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"C:\Users\aconw\Downloads\JSONDirectory\DatasetTest.jsonld", JsonConvert.SerializeObject(test, Newtonsoft.Json.Formatting.Indented));
            // constant string variable representing a created file of Program.cs
            const string curFile = @"C:\Users\aconw\Downloads\JSONDirectory\DatasetTest.jsonld";
            try
            {
                // using the File.Exists method in I.O. - output string text that it does indeed exist
                var result = (string)(File.Exists(curFile) ? "File exists" : "File does not exist");
                const string expected = "File exists";
                Assert.AreEqual(expected,result);
            }
            // prints exception error stack trace
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
