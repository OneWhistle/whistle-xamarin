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
            private set
            {
                newUser = value; RaisePropertyChanged("NewUser");
                //if (IsNewUserChanged != null)
                // IsNewUserChanged(newUser.IsMale);
            }
        }

        private bool _maleChange;
        public bool MaleChange
        {
            get { return _maleChange; }
            private set
            {
                _maleChange = value; RaisePropertyChanged(() => MaleChange);
                if (IsGenderChanged != null)
                    IsGenderChanged(0, _maleChange);
            }
        }
        private bool _femaleChange;
        public bool FemaleChange
        {
            get { return _femaleChange; }
            private set
            {
                _femaleChange = value; RaisePropertyChanged(() => FemaleChange);
                if (IsGenderChanged != null)
                    IsGenderChanged(1, _femaleChange);
            }
        }

        public Action<int, bool> IsGenderChanged { get; set; }

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
                // NOoooooooo !!!!!
                case LandingConstants.ACTION_MALE_OPTION:  //We'll definitely improve this :)
                    if (!MaleChange)
                        MaleChange = true;
                    else
                        MaleChange = false;
                    break;
                case LandingConstants.ACTION_FEMALE_OPTION:
                    if (!FemaleChange)
                        FemaleChange = true;
                    else
                        FemaleChange = false;
                    break;
                case LandingConstants.ACTION_FB_LOGIN_VALIDATE:
                case LandingConstants.ACTION_TWITTER_LOGIN_VALIDATE:
                case LandingConstants.ACTION_GOOGLE_LOGIN_VALIDATE:
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
                        DOB = new DateTime(1987, 03, 18),
                        Name = NewUser.FullName,
                        Password = NewUser.Password,
                        Phone = NewUser.Mobile,
                        Email = NewUser.Email,
                        UserName = newUser.UserName,
                        //cnfmPassword = NewUser.Password,
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
