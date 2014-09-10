// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Android.Gms.Location;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Android.Locations;
    using Android.OS;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;
    using Whistle.Core;
    using Whistle.Core.Helpers;
    using Whistle.Core.ViewModels;
    using Whistle.Droid.Fragments;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using System;

    /// <summary>
    /// Defines the MainView type.
    /// </summary>
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MainViewTheme")]
    public class MainView : WhistleSlidingFragmentActivity<HomeMessage>, GoogleMap.IOnMapLongClickListener
    {
        Android.Support.V4.App.DialogFragment _currentDialog;
        internal MapView _mapView;
        MainViewModel _viewModel;
        LocationManager _locationManager;
        string _locationProvider;
        Geocoder _geocoder;

        Marker _sourceLocationMarker;
        Marker _destinationLocationMarker;

        public new MainViewModel ViewModel
        {
            get { return this._viewModel ?? (this._viewModel = base.ViewModel as MainViewModel); }

        }
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var _icons = new int[] { Resource.Drawable.notification_green_icon, Resource.Drawable.user_account_green_icon, Resource.Drawable.preferences_green_icon, Resource.Drawable.checked_lock_green_icon, Resource.Drawable.question_mark_green_icon };
            ViewModel.USettings = DataProvider.SettingItems(_icons);
        }
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            bool isNewInstance = true;

            _locationManager = (LocationManager)GetSystemService(Android.Content.Context.LocationService);
            Criteria criteria = new Criteria { Accuracy = Android.Locations.Accuracy.Fine };
            _locationProvider = _locationManager.GetBestProvider(criteria, false);


            _mapView = new MapView(this, new GoogleMapOptions().InvokeZOrderOnTop(true))
                {
                    //http://stackoverflow.com/questions/2990191/zoom-controls-not-showing-when-using-a-mapview-with-fill-parent
                    Clickable = true
                };
            _mapView.OnCreate(savedInstanceState);
            MapsInitializer.Initialize(this);

            if (null != savedInstanceState) // check WhistleActivity
            {
                _content = SupportFragmentManager.GetFragment(savedInstanceState, "_content");
                isNewInstance = false;
            }
            SetContentView(Resource.Layout.Main);

            if (null != _content)
                SupportFragmentManager
                    .BeginTransaction()
                    .Replace(Resource.Id.contentArea, _content)
                    .Commit();
            SetBehindContentView(Resource.Layout.MenuHolder);
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.MenuFrame, new ContentMenuFragment { ViewModel = this.ViewModel })
                .Commit();


            SlidingMenu.TouchModeAbove = SlidingMenuSharp.TouchMode.Margin;
            SlidingMenu.BehindOffset = 60;
            SlidingMenu.ShadowWidth = 20;
            if (isNewInstance)
            {
                (_currentDialog = new GenericDialogFragment(Resource.Layout.UserType) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "select_user_type");
            }
            //var set = this.CreateBindingSet<MainView, MainViewModel>();
            //set.Bind(this).For(m=>m._sourceLocation).To(vm => vm.WhistleEditViewModel.SourceLocation).Apply();
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_item_switch:
                    (_currentDialog = new GenericDialogFragment(Resource.Layout.UserType) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "select_user_type");
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _mapView.Clickable = true;
            _mapView.OnResume();
            _mapView.Map.UiSettings.MyLocationButtonEnabled = true;
            _mapView.Map.UiSettings.ZoomControlsEnabled = true;
            _mapView.Map.UiSettings.CompassEnabled = true;

            _mapView.Map.SetOnMapLongClickListener(this);
            _geocoder = new Geocoder(this);

            var location = _locationManager.GetLastKnownLocation(_locationProvider);

            if (location != null)
            {
                var latng = new LatLng(location.Latitude, location.Longitude);
                // OnLocationChanged(location);
                updateMarker(latng);
                CameraUpdate zoom = CameraUpdateFactory.ZoomTo(15);
                _mapView.Map.MoveCamera(CameraUpdateFactory.NewLatLng(latng));
                _mapView.Map.AnimateCamera(zoom);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            _mapView.OnPause();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _mapView.OnSaveInstanceState(outState);
        }

        public override void SwitchContent(MvxFragment fragment)
        {
            _content = fragment;
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.contentArea, fragment)
                .Commit();
            SlidingMenu.ShowContent();
        }

        protected override void OnReceive(HomeMessage message)
        {
            /*let's see*/
            switch (message.UserAction)
            {
                case HomeConstants.NAV_USER_TYPE_SELECTED:
                    _currentDialog.Dismiss();
                    break;
                case HomeConstants.NAV_DISPLAY_LIST:
                    var viewmodel = (Whistle.Core.ViewModels.MainViewModel)this.ViewModel;
                    var itemSource = message.Parameter == "PACKAGES" ? viewmodel.PackageList : viewmodel.RideList;
                    var header = message.Parameter == "PACKAGES" ? "CHOOSE A PACKAGE" : "CHOOSE A RIDE";
                    (new ListDialogFragment(header) { ViewModel = this.ViewModel, ItemSource = itemSource }).Show(SupportFragmentManager, "select_items");
                    break;
                case HomeConstants.RESULT_WHISTLE_VALIDATION_FAILED:
                    (new GenericAlertFragment(Resource.Color.app_red_modal_color))
                        .WithIcon(Resource.Drawable.sad_face_white_icon)
                        .WithTitle(Resource.String.d_oops)
                        .WithDescription(Resource.String.d_whistle_creation_failed)
                        .Show(SupportFragmentManager, "whistle_creation_failed");                    
                    break;

                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    if (Settings.ShowWhistlersListMap)
                        SwitchContent(new MapHostFragment(_mapView, Resource.Layout.Whistlers, Resource.Menu.menu_switch) { ViewModel = this.ViewModel });
                    else
                        SwitchContent(new GenericFragment(Resource.Layout.Whistlers_list, Resource.Menu.menu_switch) { ViewModel = this.ViewModel });
                    break;
            }

        }

        private async void updateMarker(LatLng p0)
        {
            if (ViewModel.WhistleEditViewModel.SourceLocationMode && _sourceLocationMarker == null)
            {
                _sourceLocationMarker = _mapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_blue_icon)));
            }

            if (!ViewModel.WhistleEditViewModel.SourceLocationMode && _destinationLocationMarker == null)
            {
                _destinationLocationMarker = _mapView.Map.AddMarker(new MarkerOptions().SetPosition(p0).InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.whistlers_pin_red_icon)));
            }

            var marker = ViewModel.WhistleEditViewModel.SourceLocationMode ? _sourceLocationMarker : _destinationLocationMarker;

            marker.Position = p0;
            var addresses = await _geocoder.GetFromLocationAsync(p0.Latitude, p0.Longitude, 1);
            if (addresses.Count > 0)
            {
                if (ViewModel.WhistleEditViewModel.SourceLocationMode)
                    this.ViewModel.WhistleEditViewModel.SourceLocation = addresses[0].GetAddressLine(0);
                else
                    this.ViewModel.WhistleEditViewModel.DestinationLocation = addresses[0].GetAddressLine(0);
            }

        }

        public void OnMapLongClick(LatLng point)
        {
            Mvx.Trace(Cirrious.CrossCore.Platform.MvxTraceLevel.Diagnostic, "OnMapLongClick");
            updateMarker(point);
            //if (ViewModel.WhistleEditViewModel.SourceLocationMode) // 
            //{
            //    _sourceLocationMarker.SetPosition(point);
            //    //_sourceLocationMarker.Notify();
            //    var addresses = await _geocoder.GetFromLocationAsync(point.Latitude, point.Longitude, 1);
            //    if (addresses.Count > 0)
            //    {
            //        this.ViewModel.WhistleEditViewModel.SourceLocation = addresses[0].GetAddressLine(0);
            //    }
            //}
            //else
            //{
            //    _destinationLocationMarker.SetPosition(point);
            //}
            // this._mapView.Map.AddMarker(new MarkerOptions().SetPosition(point));
        }
    }
}