
using Android.Telephony;
using Whistle.Core.Services;

namespace Whistle.Droid.PlateformSpecific
{
    public class PhoneService: IPhoneService
    {
        public string GetPhoneNumber()
        {
            //TelephonyManager
            var manager = (TelephonyManager)Android.App.Application.Context.ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);           
            return manager.Line1Number;
        }

        public bool IsGlobalPhoneNumber(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 6)
                return false;
            return input.StartsWith("+") && PhoneNumberUtils.IsGlobalPhoneNumber(input);
        }

     
    }
}