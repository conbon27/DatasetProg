namespace DatasetProg
{
    internal class StationData
    {
        // creates string variables for values to be passed into from Database into JSON-LD
        // properties set for accessors
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string StationId { get; set; }
        public string Csv { get; set; }
        public string Json { get; set; }
    }
}
