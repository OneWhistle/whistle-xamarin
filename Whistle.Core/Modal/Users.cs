using Newtonsoft.Json;
using System;

namespace Whistle.Core.Modal
{
    public class Users
    {
        //public int  UserID { get; set; }

        [JsonProperty(PropertyName="email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName="username", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName="name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public string cnfmPassword { get; set; }
        [JsonProperty(PropertyName="dob", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DOB { get; set; }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }
}
