using Newtonsoft.Json;

namespace Whistle.Core.Modal
{
    public class CustomLocation
    {
        [JsonProperty(PropertyName = "type")]
        public string @Type { get; set; }
        [JsonProperty(PropertyName = "coordinates")]
        public double[] Coordinates { get; set; }

        public CustomLocation()
            : this(0, 0)
        {
        }
        public CustomLocation(double lat, double lg)
        {
            this.Type = "point";
            this.Coordinates = new double[] { lat, lg };
        }
    }
}
