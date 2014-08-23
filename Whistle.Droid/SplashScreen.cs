using Android.App;
using Android.Content.PM;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Java.Lang;

namespace Whistle.Droid
{
    [Activity(
		Label = "Whistle"
		, MainLauncher = true
        , Icon = "@drawable/whistle_logo_green"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        //public SplashScreen()
        //    : base(Resource.Layout.SplashScreen)
        //{
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Thread.Sleep(5000); // Simulate a long loading process on app startup.
            StartActivity(typeof(LandingActivity));
        }
    }
}