using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Preferences;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Whistle.Core.Helpers;

namespace Whistle.Droid
{
    /// <summary>
    /// Service class that will update the backend with the user location
    /// every X seconds.
    /// </summary>
    [Service(Enabled = true, Exported = false)]
    public class WhistleLocationService
        : Service, ILocationListener
    {
        /// <summary>
        /// Check if the service is up.
        /// </summary>
        internal static bool IsRunning;

        readonly Criteria _criteria;
        LocationManager _locationManager;
        ISharedPreferences _appSharedPreferences;
        string _locationProvider;

        public WhistleLocationService()
        {
            _criteria = new Criteria();
            _criteria.Accuracy = Accuracy.Fine;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnStartCommand WhistleLocationService");
            var result =  base.OnStartCommand(intent, flags, startId);

            _appSharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            var accessToken = _appSharedPreferences.GetString(Settings.AccessTokenKey, null);

            _locationManager = (LocationManager)GetSystemService(Android.Content.Context.LocationService);
            _locationProvider = _locationManager.GetBestProvider(_criteria, true);
            _locationManager.RequestLocationUpdates(_locationProvider, 10000, 10, this);
            IsRunning = true;

            return result;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            IsRunning = false;
        }


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public void OnLocationChanged(Location location)
        {
            var accessToken = _appSharedPreferences.GetString(Settings.AccessTokenKey, null);
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnLocationChanged {0} : {1} x {2}", accessToken, location.Latitude, location.Longitude);
            /*the settings will be updated here.*/
            /*always check if we have the accesstoken*/
            /*if missing then stopItSleft*/
        }

        public void OnProviderDisabled(string provider)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnProviderDisabled");
        }

        public void OnProviderEnabled(string provider)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnProviderEnabled");
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnStatusChanged {0} / {1}", provider, status);
        }
    }
}