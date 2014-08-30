// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Android.Util;

namespace Whistle.Droid.Views
{
    using System;
    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using Cirrious.MvvmCross.Droid.Fragging;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;
    using Cirrious.MvvmCross.Droid.Views;
    using Whistle.Core.Modal;
    using Whistle.Core.ViewModels;
    using Whistle.Droid.Fragments;

    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(NoHistory = true, Theme = "@android:style/Theme.Holo.NoActionBar")]
    public class LandingView : MvxFragmentActivity
    {

        int baseFragment;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.LandingView);

            #region Landing Fragment Handling

            try
            {
                var landingFragment = new LandingFragments(); //TODO: Need to add data
                landingFragment.LandingButtonClick += AppEntry; // or not..:)
                baseFragment = landingFragment.Id;
                SwitchScreen(landingFragment, false, true);
            }
            catch (Exception ex)
            {

                throw;
            }

            #endregion
        }

        void _clickAction(object sender, System.EventArgs e)
        {
            (this.ViewModel as Whistle.Core.ViewModels.LandingViewModel).Show();
        }

        void AppEntry(int screenID)
        {
            switch (screenID)
            {
                case 0:
                    var registrationFrag = new RegistrationFragment();
                    registrationFrag.ViewModel = ((LandingViewModel) ViewModel);
                    SwitchScreen(registrationFrag);
                    break;
                case 1:
                    var loginFrag = new LoginFragments();
                    loginFrag.SignUpButtonClick += AppEntry;
                    loginFrag.ViewModel = ((LandingViewModel) ViewModel);
                    SwitchScreen(loginFrag);
                    break;
                default:
                    break;
            }
        }
        public int SwitchScreen(MvxFragment fragment, bool animated = true, bool isRoot = false)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
           /* if (animated)
            {
                transaction.SetCustomAnimations(Resource.Animation.enter, Resource.Animation.exit);
            }*/
            transaction.Replace(Resource.Id.contentFrame, fragment); // need to add first view
            return transaction.Commit();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("baseFragment", baseFragment);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            baseFragment = savedInstanceState.GetInt("baseFragment");
        }
    }
}