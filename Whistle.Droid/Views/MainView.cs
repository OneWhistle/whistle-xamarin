// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Android.OS;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;
    using Whistle.Core;
    using Whistle.Core.Helpers;
    using Whistle.Core.ViewModels;
    using Whistle.Droid.Fragments;
    using Whistle.Droid.Helper;

    /// <summary>
    /// Defines the MainView type.
    /// </summary>
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MainViewTheme")]
    public class MainView : WhistleSlidingFragmentActivity<HomeMessage>, GoogleMap.IOnMapLongClickListener
    {
        Android.Support.V4.App.DialogFragment _currentDialog;

        MainViewModel _viewModel;
        LocationHelper<MainView> _locationHelper;
        ListItemHelper _listItemHelper;


        public new MainViewModel ViewModel { get { return this._viewModel ?? (this._viewModel = base.ViewModel as MainViewModel); } }

        public MapView MapView { get { return _locationHelper.MapView; } }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _locationHelper = new LocationHelper<MainView>(this);
            _listItemHelper = new ListItemHelper(this);
            var _icons = new int[] { Resource.Drawable.notification_green_icon, Resource.Drawable.user_account_green_icon, Resource.Drawable.preferences_green_icon, Resource.Drawable.checked_lock_green_icon, Resource.Drawable.question_mark_green_icon };
        }
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            bool isNewInstance = true;

            base.OnCreate(savedInstanceState);
            _locationHelper.OnCreate(savedInstanceState);

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
            _locationHelper.OnResume();
            _listItemHelper.OnResume();
            MapView.Map.SetOnMapLongClickListener(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationHelper.OnPause();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _locationHelper.OnSaveInstanceState(outState);
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
                    _listItemHelper.ShowList(message.Parameter);
                    break;
                case HomeConstants.RESULT_WHISTLE_VALIDATION_FAILED:
                    (new GenericAlertFragment(Resource.Color.app_red_modal_color))
                        .WithIcon(Resource.Drawable.sad_face_white_icon)
                        .WithTitle(Resource.String.d_oops)
                        .WithDescription(Resource.String.d_whistle_creation_failed)
                        .Show(SupportFragmentManager, "whistle_validation_failed");
                    break;
                case HomeConstants.RESULT_WHISTLE_CREATION_FAILED:
                    (new GenericAlertFragment(Resource.Color.app_red_modal_color))
                        .WithIcon(Resource.Drawable.sad_face_white_icon)
                        .WithTitle(Resource.String.d_oops)
                        .WithDescription(Resource.String.d_unexptected_error, message.Payload)
                        .Show(SupportFragmentManager, "whistle_creation_failed");
                    break;
                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    if (Settings.ShowWhistlersListMap)
                        SwitchContent(new MapHostFragment(MapView, Resource.Layout.Whistlers, Resource.Menu.menu_switch) { ViewModel = this.ViewModel });
                    else
                        SwitchContent(new GenericFragment(Resource.Layout.Whistlers_list, Resource.Menu.menu_switch) { ViewModel = this.ViewModel });
                    break;
                case LandingConstants.RESULT_REGISTER_SUCCESS:
                    (new GenericAlertFragment(Resource.Color.app_green_modal_color))
                        .WithIcon(Resource.Drawable.happy_face_white_icon)
                        .WithTitle(Resource.String.d_awesome)
                        .WithDescription(Resource.String.d_registration_success)
                        .Show(SupportFragmentManager, "register_success");
                    break;
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    ((MainViewModel)this.ViewModel).UserAction.Execute(LandingConstants.ACTION_REGISTER_DONE);
                    break;
            }

        }

        public void OnMapLongClick(LatLng point)
        {
            Mvx.Trace(Cirrious.CrossCore.Platform.MvxTraceLevel.Diagnostic, "OnMapLongClick");
            _locationHelper.UpdateMarkers(point);
        }


    }
}