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

    /// <summary>
    /// Defines the MainView type.
    /// </summary>
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MainViewTheme")]
    public class MainView : WhistleSlidingFragmentActivity<HomeMessage>, Android.Locations.ILocationListener
    {
        Android.Support.V4.App.DialogFragment _currentDialog;
        internal MapView _mapView;
        MainViewModel _viewModel;
        LocationManager _locationManager;
        string _locationProvider;

        public new MainViewModel ViewModel
        {
            get { return this._viewModel ?? (this._viewModel = base.ViewModel as MainViewModel); }

        }
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var _icons = new int[] { Resource.Drawable.notification_green_icon, Resource.Drawable.user_account_green_icon, Resource.Drawable.preferences_green_icon, Resource.Drawable.checked_lock_green_icon, Resource.Drawable.question_mark_green_icon };
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
            
            
            _mapView = new MapView(this);
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

            SlidingMenu.TouchModeAbove = SlidingMenuSharp.TouchMode.Fullscreen;
            SlidingMenu.BehindOffset = 60;
            SlidingMenu.ShadowWidth = 20;
            if (isNewInstance)
            {
                (_currentDialog = new GenericDialogFragment(Resource.Layout.UserType) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "select_user_type");
            }
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
            _mapView.OnResume();
            var location = _locationManager.GetLastKnownLocation(_locationProvider);
            if (location != null)
                OnLocationChanged(location);
            _locationManager.RequestLocationUpdates(_locationProvider, 1000, 10, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _mapView.OnPause();
            _locationManager.RemoveUpdates(this);
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

                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    if (Settings.ShowWhistlersListMap)
                        SwitchContent(new MapHostFragment(_mapView, Resource.Layout.Whistlers, Resource.Menu.menu_switch) { ViewModel = this.ViewModel });
                    else
                        SwitchContent(new GenericFragment(Resource.Layout.Whistlers_list, Resource.Menu.menu_switch) { ViewModel = this.ViewModel });

                    break;
            }

        }

        public void OnLocationChanged(Android.Locations.Location p0)
        {       
            if (_mapView.Map != null)
            {
                Mvx.Trace(Cirrious.CrossCore.Platform.MvxTraceLevel.Diagnostic, "Updated location");
                LatLng latLng = new LatLng(p0.Latitude, p0.Longitude);
                CameraUpdate zoom = CameraUpdateFactory.ZoomTo(15);

                 //Showing the current location in Google Map
                _mapView.Map.MoveCamera(CameraUpdateFactory.NewLatLng(latLng));
                _mapView.Map.AnimateCamera(zoom);

            }

        }


        public void OnProviderDisabled(string provider)
        {
            //  throw new System.NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //    throw new System.NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            //   throw new System.NotImplementedException();
        }
    }
}