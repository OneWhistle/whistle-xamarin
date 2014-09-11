using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Fragging;
using Cirrious.MvvmCross.Droid.Views;
using System;

namespace Whistle.Droid.Helper
{
    public interface ILocationClient
    {
        void UpdateMarkers(LatLng psitio);
    }

    public class LocationHelper<TActivity> : ILocationListener where TActivity : MvxActionBarActivity, ILocationClient
    {
        readonly Criteria _criteria;
        readonly TActivity _activity;
        LocationManager _locationManager;

        string _locationProvider;

        public MapView MapView { get; private set; }

        public LocationHelper(TActivity activity)
        {
            _criteria = new Criteria { Accuracy = Android.Locations.Accuracy.Coarse };
            _criteria.PowerRequirement = Power.Low; // Chose your desired power consumption level.
            _criteria.Accuracy = Accuracy.Fine; // Choose your accuracy requirement.
            _activity = activity;
        }

        public void OnCreate(Bundle state)
        {
            _locationManager = (LocationManager)_activity.GetSystemService(Android.Content.Context.LocationService);
            _locationProvider = _locationManager.GetBestProvider(_criteria, true);
            MapsInitializer.Initialize(_activity);
            MapView = new MapView(_activity, new GoogleMapOptions().InvokeZOrderOnTop(true).InvokeZoomControlsEnabled(true))
            {

                //http://stackoverflow.com/questions/2990191/zoom-controls-not-showing-when-using-a-mapview-with-fill-parent
                // Clickable = true
            };
            MapView.OnCreate(state);

        }


        public void OnResume()
        {
            MapView.Map.UiSettings.MyLocationButtonEnabled = true;
            MapView.Map.UiSettings.ZoomControlsEnabled = true;
            MapView.Map.UiSettings.CompassEnabled = true;
            MapView.OnResume();
            

            var location = _locationManager.GetLastKnownLocation(_locationProvider);

            if (location != null)
            {
                var latng = new LatLng(location.Latitude, location.Longitude);
                // OnLocationChanged(location);
                _activity.UpdateMarkers(latng);
                CameraUpdate zoom = CameraUpdateFactory.ZoomTo(15);
                MapView.Map.MoveCamera(CameraUpdateFactory.NewLatLng(latng));
                MapView.Map.AnimateCamera(zoom);
                return;
            }

            _locationManager.RequestSingleUpdate(_locationProvider, this, Looper.MainLooper);
        }

        public void OnPause()
        {
            MapView.OnPause();
            _locationManager.RemoveUpdates(this);
        }


       

        public void OnSaveInstanceState(Bundle outState)
        {
            MapView.OnSaveInstanceState(outState);
        }

        public void OnLocationChanged(Location location)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnLocationChanged");
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {

        }

        public IntPtr Handle { get { return _activity.Handle; } }

        public void Dispose()
        {
        }
    }
}