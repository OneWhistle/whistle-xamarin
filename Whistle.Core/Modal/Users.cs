using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class Users
    {
        //public int  UserID { get; set; }

        public string Email { get; set; }
        //public string firstName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
        public string Password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string cnfmPassword { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? dob { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string phone { get; set; }
    }
}
