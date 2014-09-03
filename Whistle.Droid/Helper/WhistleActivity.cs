

using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
namespace Whistle.Droid.Helper
{
    public abstract class WhistleActivity<TMessage> : MvxActionBarActivity where TMessage : MvxMessage
    {
        IMvxMessenger _messenger;
        MvxSubscriptionToken _subscriptionToken;

        protected Android.Support.V4.App.Fragment _content;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _subscriptionToken = _messenger.SubscribeOnMainThread<TMessage>(OnReceive);
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

        protected abstract void OnReceive(TMessage message);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _messenger.Unsubscribe<TMessage>(_subscriptionToken);
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