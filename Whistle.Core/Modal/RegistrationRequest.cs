using Newtonsoft.Json;

namespace Whistle.Core.Modal
{
    
    public class RegistrationRequest
    {
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }
    }

    public class RegistrationResponse
    {
        [JsonProperty(PropertyName = "newUser")]
        public User NewUser { get; set; }
    }
}
