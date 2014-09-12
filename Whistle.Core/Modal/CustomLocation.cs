using Newtonsoft.Json;

namespace Whistle.Core.Modal
{
    public class CustomLocation //: Point
    {
        [JsonProperty(PropertyName = "type")]
        public string @Type { get; set; }
        [JsonProperty(PropertyName = "coordinates")]
        public double?[] Coordinates { get; set; }


        public CustomLocation():this(0,0)
        {
        }
        public CustomLocation(double lat, double lg)
        {
            this.Type = "point";
            this.Coordinates = new double?[] { lat, lg };
        }
        //[JsonProperty(PropertyName = "coordinates", NullValueHandling = NullValueHandling.Ignore)]
        //public new IPosition Coordinates
        //{
        //    get { return base.Coordinates; }
        //    set { base.Coordinates = value; }
        //}

        //public CustomLocation()
        //    : base(new GeographicPosition(0, 0))
        //{

        //}

        //public CustomLocation(IPosition coordinates) :
        //    base(coordinates)
        //{

        //}
    }
}
