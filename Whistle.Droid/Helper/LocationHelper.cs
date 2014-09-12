using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging;
using Cirrious.MvvmCross.Droid.Views;
using System;
using Whistle.Core.ViewModels;

namespace Whistle.Droid.Helper
{


    public class LocationHelper<TActivity> : ILocationListener where TActivity : MvxActionBarActivity
    {
        readonly Criteria _criteria;
        readonly TActivity _activity;
        LocationManager _locationManager;
        Marker _sourceLocationMarker;
        Marker _destinationLocationMarker;
        Geocoder _geocoder;

        string _locationProvider;

        public MapView MapView { get; private set; }

        protected MainViewModel ViewModel { get { return _activity.ViewModel as MainViewModel; } }

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
            _geocoder = new Geocoder(_activity);
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
                //OnLocationChanged(location);
                UpdateMarkers(latng);
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


        public async void UpdateMarkers(LatLng p0)
        {
            if (ViewModel.WhistleEditViewModel.SourceLocationMode && _sourceLocationMarker == null)
            {
                _sourceLocationMarker = MapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_blue_icon)));                
            }

            if (!ViewModel.WhistleEditViewModel.SourceLocationMode && _destinationLocationMarker == null)
            {
                _destinationLocationMarker = MapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_red_icon)));
            }

            ViewModel.WhistleEditViewModel.UpdatePosition(p0.Latitude, p0.Longitude);
            var marker = ViewModel.WhistleEditViewModel.SourceLocationMode ? _sourceLocationMarker : _destinationLocationMarker;

            marker.Position = p0;
            try
            {
                var addresses = await _geocoder.GetFromLocationAsync(p0.Latitude, p0.Longitude, 1);
                if (addresses.Count > 0)
                {
                    if (ViewModel.WhistleEditViewModel.SourceLocationMode)
                        this.ViewModel.WhistleEditViewModel.SourceLocation = addresses[0].GetAddressLine(0);
                    else
                        this.ViewModel.WhistleEditViewModel.DestinationLocation = addresses[0].GetAddressLine(0);
                }
            }
            catch (Exception ex)
            {
                Mvx.Trace(MvxTraceLevel.Diagnostic, ex.Message);
            }

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
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnProviderDisabled");
        }

        public void OnProviderEnabled(string provider)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnProviderEnabled");
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnStatusChanged");
        }

        public IntPtr Handle { get { return _activity.Handle; } }

        public void Dispose()
        {
        }
    }
}