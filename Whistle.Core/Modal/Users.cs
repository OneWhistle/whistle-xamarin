using Newtonsoft.Json;
using System;
using Whistle.Core.Helpers;
using Whistle.Core.Services;

namespace Whistle.Core.Modal
{
    public class User : BaseEntity
    {
        //public int  UserID { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "username", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        private DateTime? _dob;

        [JsonProperty(PropertyName = "dob", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DOB { get { return _dob; } set { _dob = value; DOBStr = _dob.Value.ToString("d"); OnPropertyChanged(""); OnPropertyChanged("DOB"); } }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "location", NullValueHandling = NullValueHandling.Ignore)]
        public CustomLocation Location { get; set; }

        string _gender;
        [JsonProperty(PropertyName = "gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get { return _gender; } set { _gender = value; isMale = _gender == "male" ? true : false; } }
        //Yes, We'll use single ahead, based on param

        [JsonIgnore]
        public string DOBStr
        {
            get;
            set;
        }

        private bool? isMale;
        [JsonIgnore]
        public bool? IsMale
        {
            get { return isMale; }
            set { isMale = value; Gender = isMale.Value ? "male" : "female"; OnPropertyChanged("IsMale"); OnPropertyChanged("IsFemale"); }
        }

        [JsonIgnore]
        public bool? IsFemale { get { return IsMale.Value ? false : true; } }
    }

    public class PasswordResponse
    {
        [JsonProperty(PropertyName = "key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonIgnore]
        public bool Disable { get; set; }
    }

    public class PasswordReset
    {
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "passwordResetKey", NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordKey { get; set; }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonIgnore]
        public string ConfPassword { get; set; }
    }

    public class PasswordResetRequest
    {
        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }

    public class PasswordResetResponse
    {
        [JsonProperty(PropertyName = "ok", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsChanged { get; set; }
    }
}
