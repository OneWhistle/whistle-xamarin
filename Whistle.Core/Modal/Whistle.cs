﻿using Newtonsoft.Json;

namespace Whistle.Core.Modal
{
    public class Whistle
    {
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
    }
}