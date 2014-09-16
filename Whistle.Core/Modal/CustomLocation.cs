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
        //beware of the order (longitude , latitude)
        // https://github.com/ramaprasanna/whistle/issues/20
        public CustomLocation(double longitude, double latitude)
        {
            this.Type = "point";
            this.Coordinates = new double[] { longitude, latitude };
        }
    }
}
