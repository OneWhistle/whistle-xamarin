// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Droid.Fragging;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using SlidingMenuSharp.App;
    using Whistle.Core;
    using Whistle.Droid.Fragments;

    /// <summary>
    /// Defines the MainView type.
    /// </summary>
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MainViewTheme")]
    public class MainView : WhistleSlidingFragmentActivity<HomeMessage>
    {
        Android.Support.V4.App.DialogFragment _currentDialog;
        
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            bool isNewInstance = true;

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
            SlidingMenu.BehindOffset = 80;
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
            }

        }
    }
}