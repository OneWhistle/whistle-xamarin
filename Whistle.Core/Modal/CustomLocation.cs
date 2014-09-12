using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class CustomLocation : Point
    {
        [JsonProperty(PropertyName = "coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public new IPosition Coordinates
        {
            get { return base.Coordinates; }
            set { base.Coordinates = value; }
        }

        public CustomLocation()
            : base(new GeographicPosition(0, 0))
        {

        }

        public CustomLocation(IPosition coordinates) :
            base(coordinates)
        {

        }
    }
}
