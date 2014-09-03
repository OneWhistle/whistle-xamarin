// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Whistle.Core;

namespace Whistle.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Droid.Fragging;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using MeetupManager.Droid.Helpers;
    using Whistle.Core.ViewModels;
    using Whistle.Droid.Fragments;




    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/LandingViewTheme")]
    public class LandingView : MvxActionBarActivity
    {
        IMvxMessenger _messenger;
        MvxSubscriptionToken _subscriptionToken;

        int baseFragment;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _subscriptionToken = _messenger.SubscribeOnMainThread<LandingMessage>(OnReceive);
        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount == 1) // landing fragment
            {
                Finish();
                return;
            }
            SupportFragmentManager.PopBackStackImmediate();
            if (SupportFragmentManager.BackStackEntryCount == 1) // landing fragment
            {
                SupportActionBar.Hide();
            }
        }

        protected void OnReceive(LandingMessage message)
        {
            if (!SupportActionBar.IsShowing)
                SupportActionBar.Show();
            switch (message.UserAction)
            {
                case LandingConstants.ACTION_REGISTER:
                    SupportActionBar.Title = "Create an account";
                    SwitchScreen(new GenericFragment(Resource.Layout.Registration) { ViewModel = this.ViewModel }, "registration");
                    break;
                case LandingConstants.ACTION_SIGNIN:
                    SupportActionBar.Title = "Sign in";
                    SwitchScreen(new GenericFragment(Resource.Layout.Login) { ViewModel = this.ViewModel }, "signin");
                    break;
                case LandingConstants.ACTION_FORGOT_PASSWORD:
                    (new GenericDialogFragment(Resource.Layout.ForgetPassword) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "forgot_password");
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                    SwitchScreen(new GenericFragment(Resource.Layout.ServiceOptions) { ViewModel = this.ViewModel }, "register_continue");
                    break;

                case LandingConstants.RESULT_LOGIN_FAILED:
                    (new GenericDialogFragment(Resource.Layout.WrongPassword, Resource.Color.app_red_modal_color) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "wrong_password");
                    ((LandingViewModel)ViewModel).IsBusy = false;
                    break;
                // Others are handled by the view model
                default:
                    break;
            }
        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //SupportActionBar.SetHomeButtonEnabled
            this.SetContentView(Resource.Layout.LandingView);
            this.SupportActionBar.SetHomeButtonEnabled(true);
        }

        protected override void OnResumeFragments()
        {
            base.OnResumeFragments();
            SwitchScreen(new GenericFragment(Resource.Layout.Landing) { ViewModel = this.ViewModel }, "landing");
            SupportActionBar.Hide();
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }


        protected void SwitchScreen(MvxFragment fragment, string stackInfo)
        {
            SupportFragmentManager.BeginTransaction()
                 .Replace(Resource.Id.contentFrame, fragment)
                 .AddToBackStack(stackInfo)
                 .Commit();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _messenger.Unsubscribe<LandingMessage>(_subscriptionToken);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("baseFragment", baseFragment);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            baseFragment = savedInstanceState.GetInt("baseFragment");
        }
    }
}