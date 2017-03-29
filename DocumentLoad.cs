using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatasetProg
{
    // classes created to represent each attribute of json-ld end file
    public class ContactPoint
    {
        // JsonProperty used to include @ sign in final tags to enable discoverability
        [JsonProperty("@type")]
        public string type { get; set; }

        public string contactType { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
    }

    public class Creator
    {
        [JsonProperty("@type")]
        public string type { get; set; }

        public string url { get; set; }
        public string name { get; set; }
        public ContactPoint contactPoint { get; set; }
    }

    public class IncludedInDataCatalog
    {
        [JsonProperty("@type")]
        public string type { get; set; }

        public string name { get; set; }
    }

    public class Distribution
    {
        [JsonProperty("@type")]
        public string type { get; set; }

        public string encodingFormat { get; set; }
        public string contentUrl { get; set; }
    }

    public class Geo
    {
        [JsonProperty("@type")]
        public string type { get; set; }

        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class SpatialCoverage
    {
        [JsonProperty("@type")]
        public string type { get; set; }

        public Geo geo { get; set; }
    }

    public class Dataset
    {
        [JsonProperty("@context")]
        public string context { get; set; }

        [JsonProperty("@type")]
        public string type { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string sameAs { get; set; }
        public IList<string> keywords { get; set; }
        public Creator creator { get; set; }
        public IncludedInDataCatalog includedInDataCatalog { get; set; }
        public IList<Distribution> distribution { get; set; }
        public string temporalCoverage { get; set; }
        public SpatialCoverage spatialCoverage { get; set; }
    }
}