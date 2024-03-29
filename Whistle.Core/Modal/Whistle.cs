﻿using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;

namespace Whistle.Core.Modal
{
    public class Whistle
    {
        //[JsonProperty(PropertyName = "leavingLocation")]
        //public Point LeavingLocation { get; set; }
        [JsonIgnore]
        //(PropertyName = "destinationLocation")]
        public CustomLocation DestinationLocation { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string @Type { get; set; }
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "public")]
        public bool @Public { get; set; }
        [JsonProperty(PropertyName = "provider")]
        public bool Provider { get; set; }
        [JsonProperty(PropertyName = "size")]
        public int[] Size { get; set; }

        [JsonProperty(PropertyName = "matchingWhisltes", NullValueHandling = NullValueHandling.Ignore)]
        public MatchingWhistle[] MatchingWhisltes { get; set; }
    }
}
