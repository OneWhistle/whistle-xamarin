using Newtonsoft.Json;
using System;

namespace Whistle.Core.Modal
{
    public class User : BaseEntity
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

        private string _dob;

        [JsonProperty(PropertyName="dob", NullValueHandling = NullValueHandling.Ignore)]
        public string DOB { get { return _dob; } set { _dob = value; OnPropertyChanged("DOB"); } }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }


        public string AccessToken { get; set; }

        //Yes, We'll use single ahead, based on param
        public bool? IsMale { get; set; }
    }
}
