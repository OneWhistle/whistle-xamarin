using Newtonsoft.Json;

namespace Whistle.Core.Modal
{
    public class CreateWhistleRequest
    {
        [JsonProperty(PropertyName = "whistle")]
        public Whistle Whistle { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "radius")]
        public int Radius { get; set; }
        //public bool IsNewWhistle { get; set; }

        public CreateWhistleRequest()
        {
            Limit = 10;
         //   IsNewWhistle = true;
            Radius = 10;

        }
    }
}
