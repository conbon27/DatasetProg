﻿using System;

namespace DatasetProg
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // creates a new URL object
            var u = new Url();
            // connects to MySQL DB to read data
            var results = u.ReadTable();
            // creates a new detail object
            var test1 = new Detail();
            // populates details & outputs to console before creating JSON-LD
            test1.ConfirmDetails(results);
            //test1.ListAntics();
            Console.ReadKey();
        }
    }
}