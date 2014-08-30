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
    using Whistle.Droid.Fragments;


    

    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/LandingViewTheme")]
    public class LandingView : MvxFragmentActivity
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
        }

        protected void OnReceive(LandingMessage message)
        {
            switch (message.UserAction)
            {
                case LandingConstants.ACTION_REGISTER:
                    SwitchScreen(new GenericFragment(Resource.Layout.Registration) { ViewModel = this.ViewModel }, "registration");
                    break;
                case LandingConstants.ACTION_SIGNIN:
                    SwitchScreen(new GenericFragment(Resource.Layout.Login) { ViewModel = this.ViewModel }, "signin");
                    break;
                case LandingConstants.ACTION_FORGOT_PASSWORD:
                    SwitchScreen(new GenericFragment(Resource.Layout.ForgetPassword) { ViewModel = this.ViewModel }, "forgot_password");
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                    SwitchScreen(new GenericFragment(Resource.Layout.ServiceOptions) { ViewModel = this.ViewModel }, "register_continue");
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
            this.SetContentView(Resource.Layout.LandingView);
            base.OnCreate(bundle);
        }

        protected override void OnResumeFragments()
        {
            base.OnResumeFragments();
            SwitchScreen(new GenericFragment(Resource.Layout.Landing) { ViewModel = this.ViewModel }, "landing");
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