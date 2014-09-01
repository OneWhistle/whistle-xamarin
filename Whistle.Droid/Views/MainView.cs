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
    using Cirrious.MvvmCross.Droid.Fragging;
    using SlidingMenuSharp.App;
    using Whistle.Droid.Fragments;

    /// <summary>
    /// Defines the MainView type.
    /// </summary>
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MainViewTheme")]
    public class MainView : WhistleSlidingFragmentActivity
    {
        private Android.Support.V4.App.Fragment _content;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (null != savedInstanceState)
                _content = SupportFragmentManager.GetFragment(savedInstanceState, "_content");

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
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            SupportFragmentManager.PutFragment(outState, "_content", _content);
        }

        public override void SwitchContent(Android.Support.V4.App.Fragment fragment)
        {
            _content = fragment;
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.contentArea, fragment)
                .Commit();
            SlidingMenu.ShowContent();
        }
    }
}