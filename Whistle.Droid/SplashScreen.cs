using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;
using Mindscape.Raygun4Net;

namespace Whistle.Droid
{
    [Activity(
		Label = "Whistle"
		, MainLauncher = true
        , Icon = "@drawable/whistle_logo_green"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
            //RaygunClient.Attach("FtX2mlgWsSYKn4uOA55pJQ==");
        }
    }
}