using Whistle.Core.Modal;
using Whistle.Core.Services;

namespace Whistle.Core.ViewModels
{
    public static class UserExtensions
    {

        public static string[] IsValid(this User user, IPhoneService phone)
        {
            /*It should be better that that..*/
            if (string.IsNullOrEmpty(user.Password))
                return new[] { "missing password" };
            //if (string.IsNullOrEmpty(user.Email))
            //    return false;
            if (string.IsNullOrEmpty(user.Name))
                return new[] { "missing full name" };
            if (string.IsNullOrEmpty(user.UserName))
                return new[] { "missing username" };
            if (null == user.DOB)
                return new[] { "missing day of birth" };

            if (!phone.IsGlobalPhoneNumber(user.Phone))
                return new[] { "invalid phone number.\r\nPlease provide in international format (e.g: +1889123456)" };

            return new string[] { };
        }

    }

}
