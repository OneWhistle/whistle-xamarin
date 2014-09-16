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
using System.Collections.Generic;
using Whistle.Core.ViewModels;
using System.Linq;

namespace Whistle.Droid.Helper
{


    public class LocationHelper<TActivity>  where TActivity : MvxActionBarActivity, ILocationListener
    {
        readonly Criteria _criteria;
        readonly TActivity _activity;
        LocationManager _locationManager;
        Marker _sourceLocationMarker;
        Marker _destinationLocationMarker;
        Geocoder _geocoder;
        readonly List<Marker> _markerList = new List<Marker>();

        string _locationProvider;

        public MapView MapView { get; private set; }

        protected MainViewModel ViewModel { get { return _activity.ViewModel as MainViewModel; } }

        public LocationHelper(TActivity activity)
        {
            _criteria = new Criteria();
            // _criteria.PowerRequirement = Power.; // Chose your desired power consumption level.
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
            Mvx.Trace(MvxTraceLevel.Diagnostic, "OnResume location helper");
            MapView.Map.UiSettings.MyLocationButtonEnabled = true;
            MapView.Map.UiSettings.ZoomControlsEnabled = true;
            MapView.Map.UiSettings.CompassEnabled = true;
            MapView.OnResume();


            _locationManager.RequestLocationUpdates(_locationProvider, 15000, 20, _activity, Looper.MainLooper);

            var location = _locationManager.GetLastKnownLocation(_locationProvider);

            if (location != null) 
            {
                var latng = new LatLng(location.Latitude, location.Longitude);
                OnLocationChanged(location);
                return;
            }

        }

        public void OnPause()
        {
            MapView.OnPause();
            _locationManager.RemoveUpdates(_activity);
        }


        public void UpdateMarkers(LatLng p0, bool source)
        {
            if (source && _sourceLocationMarker == null)
            {
                _sourceLocationMarker = MapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_blue_icon)));
            }

            if (!source && _destinationLocationMarker == null)
            {
                _destinationLocationMarker = MapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_red_icon)));
            }

            ViewModel.WhistleEditViewModel.UpdatePosition(p0.Longitude, p0.Latitude, source);
            var marker = source ? _sourceLocationMarker : _destinationLocationMarker;
            marker.Position = p0;
            UpdateLocationString(p0, source);
        }


        public void ShowWhistlers()
        {
            if (_markerList.Count > 0 || ViewModel.WhistleResultViewModel.WhistleList == null)
                return;
            foreach (var item in ViewModel.WhistleResultViewModel.WhistleList)
            {
                var p0 = new LatLng(item.Obj.Location.Coordinates[0], item.Obj.Location.Coordinates[1]);

                var marker = MapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_grey_icon)));
                _markerList.Add(marker);
            }
        }

        public void ClearWhistlers()
        {
            foreach (var marker in _markerList)
            {
                marker.Remove();
            }
            _markerList.Clear();
        }

        public async void UpdateLocationString(LatLng p0, bool source)
        {
            try
            {
                var addresses = await _geocoder.GetFromLocationAsync(p0.Latitude, p0.Longitude, 1);
                if (addresses.Count > 0)
                {
                    if (source)
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
            ViewModel.UpdateUserLocation(location.Longitude, location.Latitude);
            var latng = new LatLng(location.Latitude, location.Longitude);

            UpdateMarkers(latng, true);
            CameraUpdate zoom = CameraUpdateFactory.ZoomTo(15);
            MapView.Map.MoveCamera(CameraUpdateFactory.NewLatLng(latng));
            MapView.Map.AnimateCamera(zoom);
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

        public bool OnMarkerClick(Marker marker)
        {
            if (_markerList.Select(c=>c.Id).Any(id=> id == marker.Id))
            {
                ViewModel.ShowWhistler();
                return true;
            }
            return false;
        }
    }
}