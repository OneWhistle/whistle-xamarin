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
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Whistle.Core.ViewModels;
    using Whistle.Droid.Fragments;
    using Whistle.Droid.Helper;


    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/LandingViewTheme")]
    public class LandingView : WhistleActivity<LandingMessage>
    {
        protected override void OnReceive(LandingMessage message)
        {
            if (!SupportActionBar.IsShowing)
                SupportActionBar.Show();
            var viewModel = (BaseViewModel)this.ViewModel;
            switch (message.UserAction)
            {
                case LandingConstants.ACTION_REGISTER:
                    viewModel.Title = "CREATE AN ACCOUNT";
                    SwitchScreen(new GenericFragment(Resource.Layout.Registration, Resource.Menu.menu_help) { ViewModel = this.ViewModel }, "registration");
                    break;
                case LandingConstants.ACTION_SIGNIN:
                    viewModel.Title = "SIGN IN";
                    SwitchScreen(new GenericFragment(Resource.Layout.Login, Resource.Menu.menu_help) { ViewModel = this.ViewModel }, "signin");
                    break;
                case LandingConstants.ACTION_FORGOT_PASSWORD:
                    (new GenericDialogFragment(Resource.Layout.ForgetPassword) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "forgot_password");
                    //(new GenericAlertFragment(new AlertHelper() { IconID = Resource.Drawable.delivery_report, Description = "Test", LayoutID = Resource.Layout.AlertDialog })).Show(SupportFragmentManager, "Test");
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                    SwitchScreen(new GenericFragment(Resource.Layout.ServiceOptions, Resource.Menu.menu_help) { ViewModel = this.ViewModel }, "register_continue");
                    break;
                case LandingConstants.RESULT_LOGIN_FAILED:
                    (new GenericDialogFragment(Resource.Layout.WrongPassword, Resource.Color.app_red_modal_color) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "wrong_password");
                    ((LandingViewModel)ViewModel).IsBusy = false;
                    break;
                case LandingConstants.ACTION_PROFILE_IMAGE:
                    (new GenericDialogFragment(Resource.Layout.MediaChooser) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "media_chooser");
                    break;
                // Others are handled by the view model
                default:
                    break;
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var busyFrag = new GenericDialogFragment(Resource.Layout.BusyIndicator) { ViewModel = this.ViewModel };
            ((BaseViewModel)ViewModel).IsBusyChanged = (busy) =>
            {
                if (busy)
                    busyFrag.Show(SupportFragmentManager, "BusyIndicator");
                else
                    busyFrag.Dialog.Hide();
            };
        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.LandingView);
            this.SupportActionBar.SetHomeButtonEnabled(true);
        }

        protected override void OnResumeFragments()
        {
            base.OnResumeFragments();
            SwitchScreen(new GenericFragment(Resource.Layout.Landing, Resource.Menu.menu_help) { ViewModel = this.ViewModel }, "landing");
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
            _content = fragment as Android.Support.V4.App.Fragment;
            SupportFragmentManager.BeginTransaction()
                 .Replace(Resource.Id.contentFrame, fragment)
                 .AddToBackStack(stackInfo)
                 .Commit();
        }
    }
}