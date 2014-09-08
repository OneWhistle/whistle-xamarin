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
    using System;
    using Whistle.Core.ViewModels;
    using Whistle.Droid.Fragments;
    using Whistle.Droid.Helper;


    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/LandingViewTheme")]
    public class LandingView : WhistleActivity<LandingMessage>
    {
        protected override async void OnReceive(LandingMessage message)
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
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                    SwitchScreen(new GenericFragment(Resource.Layout.ServiceOptions, Resource.Menu.menu_help) { ViewModel = this.ViewModel }, "register_continue");
                    break;

                case LandingConstants.RESULT_LOGIN_FAILED:
                    var dialog = (new GenericAlertFragment(Resource.Color.app_red_modal_color))
                        .WithIcon(Resource.Drawable.sad_face_white_icon)
                        .WithTitle(Resource.String.d_oops);
                    if (message.HasPayload)
                        dialog = dialog.WithDescription(message.Payload);
                    else
                        dialog = dialog.WithDescription(Resource.String.d_wrong_password);
                    dialog.Show(SupportFragmentManager, "wrong_password");
                    break;

                case LandingConstants.ACTION_PROFILE_IMAGE:
                    (new GenericDialogFragment(Resource.Layout.MediaChooser) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "media_chooser");
                    break;
                case LandingConstants.RESULT_REGISTER_SUCCESS:
                    (new GenericAlertFragment(Resource.Color.app_green_modal_color))
                        .WithIcon(Resource.Drawable.happy_face_white_icon)
                        .WithTitle(Resource.String.d_awesome)
                        .WithDescription(Resource.String.d_registration_success)
                        .Show(SupportFragmentManager, "register_success");

                    await System.Threading.Tasks.Task.Delay(1500);
                    ((LandingViewModel)this.ViewModel).Show();
                    break;
                case LandingConstants.ACTION_DOB_OPTION:
                    (new GenericDialogFragment(this) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "datePicker");
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