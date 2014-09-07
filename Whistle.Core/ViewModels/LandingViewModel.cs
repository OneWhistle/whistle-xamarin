// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq.Expressions;
using Whistle.Core.Modal;

namespace Whistle.Core.ViewModels
{
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using System.Linq;
    using System;
    using Whistle.Core.Services;
    using Whistle.Core.Helper;

    /// <summary>
    /// Define the LandingViewModel type.
    /// </summary>
    public class LandingViewModel : BaseViewModel
    {
        #region Private fields
        IMvxMessenger _messenger;
        IAuthenticationService _authService;
        IRegistrationService _regService;

        #endregion

        #region Properties

        private UserViewModel newUser;
        public UserViewModel NewUser
        {
            get { return newUser; }
            private set { newUser = value; RaisePropertyChanged("NewUser"); }
        }
        #endregion

      
        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand<string> userAction;
        /// <summary>
        /// Gets user action command.
        /// </para>
        /// </summary>
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand<string>(OnUserAction)); } }


        /// <summary>
        /// Show the view model.
        /// </summary>
        public void Show()
        {
            // if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            this.ShowViewModel<MainViewModel>();
        }

        private void OnUserAction(string action)
        {
            var messenger = Mvx.Resolve<IMvxMessenger>();
            if (!LandingConstants.ActionList.Contains(action))
                throw new InvalidOperationException();
            switch (action)
            {
                case LandingConstants.ACTION_LOGIN_VALIDATE:
                    onLogin();
                    break;
                case LandingConstants.ACTION_REGISTER_DONE:
                    //testing
                    if (!NewUser.IsValid())
                    {
                        _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_LOGIN_FAILED));
                        NewUser = new UserViewModel();
                        return;
                    }
                    onRegister();
                    break;
                case LandingConstants.ACTION_FB_LOGIN_VALIDATE:
                case LandingConstants.ACTION_TWITTER_LOGIN_VALIDATE:
                case LandingConstants.ACTION_GOOGLE_LOGIN_VALIDATE:
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    this.Show();
                    break;
                default:
                    _messenger.Publish(new LandingMessage(this, action));
                    break;
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            this.NewUser = new UserViewModel();
            if (_messenger != null)
                return;
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _authService = Mvx.Resolve<IAuthenticationService>();
            _regService = Mvx.Resolve<IRegistrationService>();
        }
        protected async void onLogin()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<Users>(new Users { Email = newUser.UserName, Password = newUser.Password }, ApiAction.LOGIN);
            IsBusy = false;
            if (!result.Success)
            {
                _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_LOGIN_FAILED));
                NewUser = new UserViewModel();
                return;
            }
            else
            {
                this.Show();
            }
        }

        protected async void onRegister()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction(new RegistrationRequest
            {
                user = new Users
                    {
                        dob = new DateTime(1987, 03, 18),
                        name = NewUser.FullName,
                        password = NewUser.Password,
                        phone = NewUser.Mobile,
                        email = NewUser.Email
                        DOB = new DateTime(1987, 03, 18),
                        //firstName = "Mohd",
                        Name = NewUser.FullName,
                        UserName = newUser.UserName,
                        Password = NewUser.Password,
                        //cnfmPassword = NewUser.Password,
                        Phone = NewUser.Mobile,
                        Email = NewUser.Email
                    }
            }, ApiAction.REGISTRATION);
            IsBusy = false;

            if (!result.Success)
            {
                _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_LOGIN_FAILED));
            }
            else
            {
                _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_REGISTER_SUCCESS));
            }
            NewUser = new UserViewModel();
        }
    }
}
