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
    public class LandingView : WhistleActivity<MessageHandler>
    {
        ListItemHelper _listItemHelper;
        protected override void OnViewModelSet()
        {

            base.OnViewModelSet();
            _listItemHelper = new ListItemHelper(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            _listItemHelper.OnResume();
        }
        protected override void OnReceive(MessageHandler message)
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
                case LandingConstants.RESULT_REGISTER_VALIDATION_FAILED:
                    (new GenericAlertFragment(Resource.Color.app_red_modal_color)
                        .WithIcon(Resource.Drawable.sad_face_white_icon)
                        .WithTitle(Resource.String.d_oops))
                        .WithDescription(Resource.String.d_invalid_registration_format, message.Payload)
                        .Show(SupportFragmentManager, "invalid_registration_input");
                    break;
                case LandingConstants.RESULT_BACKEND_ERROR:
                    (new GenericAlertFragment(Resource.Color.app_red_modal_color))
                        .WithIcon(Resource.Drawable.sad_face_white_icon)
                        .WithTitle(Resource.String.d_oops)
                        .WithDescription(Resource.String.d_unexptected_error, message.Payload)
                        .Show(SupportFragmentManager, "back_end_error");
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
                    break;
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    (new GenericAlertFragment(Resource.Color.app_gray_modal_color))
                        .WithIcon(Resource.Drawable.agree_terms_green_icon)
                        .WithTitle(Resource.String.d_legal)
                        .WithDescription(Resource.String.d_legal_term_desc)
                        .AddButton(Resource.String.d_btn_agree, () => { ((LandingViewModel)this.ViewModel).UserAction.Execute(LandingConstants.ACTION_REGISTER_DONE); })
                        .AddButton(Resource.String.d_btn_disagree, () => { })
                        .Show(SupportFragmentManager, "show_legal_terms");
                    break;
                case LandingConstants.ACTION_DOB_OPTION:
                    (new GenericDialogFragment(this) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "datePicker");
                    break;
                case LandingConstants.ACTION_ENTER_CODE:
                    (new GenericDialogFragment(Resource.Layout.EnterCode) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "enter_code");
                    break;
                case LandingConstants.ACTION_GENERATE_PASSWORD:
                    (new GenericDialogFragment(Resource.Layout.ResetPassword) { ViewModel = this.ViewModel }).Show(SupportFragmentManager, "reset_password");
                    break;
                case LandingConstants.RESULT_RESET_PASSWORD_SUCCESS:
                    (new GenericAlertFragment(Resource.Color.app_green_modal_color))
                        .WithIcon(Resource.Drawable.happy_face_white_icon)
                        .WithTitle(Resource.String.d_awesome)
                        .WithDescription(Resource.String.d_password_reset_success)//.AddButton(Resource.String.d_btn_login, () => { ((LandingViewModel)this.ViewModel).UserAction.Execute(LandingConstants.ACTION_SIGNIN); })
                        .Show(SupportFragmentManager, "resetpassword_success");
                    break;
                case LandingConstants.ACTION_USER_SELECTION:
                    _listItemHelper.ShowList(message.Parameter);
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