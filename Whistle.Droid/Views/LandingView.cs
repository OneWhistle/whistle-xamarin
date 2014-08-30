// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingView type.
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
    using Whistle.Core;
    using Whistle.Droid.Fragments;

    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LandingView : MvxFragmentActivity
    {
        IMvxMessenger _messenger;

        int baseFragment;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _messenger.SubscribeOnMainThread<LandingMessage>(onReceive);
        }

        protected void onReceive(LandingMessage message)
        {
            switch (message.UserAction)
            {
                case LandingConstants.ACTION_REGISTER:
                    SwitchScreen(new GenericFragment(Resource.Layout.Registration) { ViewModel = this.ViewModel });
                    break;
                case LandingConstants.ACTION_SIGNIN:
                    SwitchScreen(new GenericFragment(Resource.Layout.Login) { ViewModel = this.ViewModel });
                    break;
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

            SwitchScreen(new GenericFragment(Resource.Layout.Landing) { ViewModel = this.ViewModel });
            base.OnCreate(bundle);
        }

        protected int SwitchScreen(MvxFragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment); // need to add first view
            return transaction.Commit();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("baseFragment", baseFragment);
            base.OnSaveInstanceState(outState);
        }


        protected override void OnResumeFragments()
        {
            base.OnResumeFragments();
        }


        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            baseFragment = savedInstanceState.GetInt("baseFragment");
        }
    }
}