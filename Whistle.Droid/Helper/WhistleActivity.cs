

using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Whistle.Droid.Fragments;
using Whistle.Core.ViewModels;
using Android.Util;
using Android.Gms.Analytics;
namespace Whistle.Droid.Helper
{
    public abstract class WhistleActivity<TMessage> : MvxActionBarActivity where TMessage : MvxMessage
    {
        IMvxMessenger _messenger;
        MvxSubscriptionToken _subscriptionToken;
        GenericDialogFragment busyFrag;

        protected Android.Support.V4.App.Fragment _content;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _messenger = Mvx.Resolve<IMvxMessenger>();            
            busyFrag = (new GenericAlertFragment(Resource.Color.app_gray_modal_color)).WithIcon(Resource.Drawable.busy).WithTitle(Resource.String.d_waiting);
            // busyFrag.
            //Adding Busy view
            ((BaseViewModel)ViewModel).IsBusyChanged = (busy) =>
            {
                Log.Info("WhistleActivity", "received busy changed");
                if (busy)
                    busyFrag.Show(SupportFragmentManager, "BusyIndicator");
                else
                {
                    busyFrag.Dismiss();
                }
            };
        }


        protected override void OnResume()
        {
            base.OnResume();
            _subscriptionToken = _messenger.SubscribeOnMainThread<TMessage>(OnReceive);
        }

        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var actionbar = this.BindingInflate(Resource.Layout.custom_action_bar, null);
            Android.Support.V7.App.ActionBar.LayoutParams lp = new Android.Support.V7.App.ActionBar.LayoutParams(Android.Support.V7.App.ActionBar.LayoutParams.MatchParent, Android.Support.V7.App.ActionBar.LayoutParams.MatchParent);
            SupportActionBar.SetDisplayShowCustomEnabled(true);
            SupportActionBar.SetCustomView(actionbar, lp);

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



        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }

        protected abstract void OnReceive(TMessage message);

        protected override void OnStop()
        {
            base.OnStop();
            _messenger.Unsubscribe<TMessage>(_subscriptionToken);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            //   SupportFragmentManager.PutFragment(outState, "_content", _content);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            _content = SupportFragmentManager.GetFragment(savedInstanceState, "_content");
        }
    }
}