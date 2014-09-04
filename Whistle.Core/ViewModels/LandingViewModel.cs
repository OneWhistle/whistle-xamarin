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

        private string userName;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
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

        private async void OnUserAction(string action)
        {
            var messenger = Mvx.Resolve<IMvxMessenger>();
            if (!LandingConstants.ActionList.Contains(action))
                throw new InvalidOperationException();
            switch (action)
            {
                case LandingConstants.ACTION_LOGIN_VALIDATE:
                    IsBusy = true;
                    var result = await ServiceHandler.PostAction<Users>(new Users { email = UserName, password = Password }, ApiAction.LOGIN);
                    if (!result.Success)
                    {
                        _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_LOGIN_FAILED));
                        return;
                    }
                    break;

                case LandingConstants.ACTION_REGISTER_DONE:
                    //testing
                    await ServiceHandler.PostAction(new Users()
                    {
                        dob = new DateTime(1987, 03, 18),
                        firstName = "Mohd",
                        lastName = "RIYAZ77",
                        password = "IAm7MOM",
                        cnfmPassword = "IAm7MOM",
                        phone = "970000000",
                        email = "rze7@whistle.com"
                    }, ApiAction.REGISTRATION);
                    break;
                case LandingConstants.ACTION_FB_LOGIN_VALIDATE:
                case LandingConstants.ACTION_TWITTER_LOGIN_VALIDATE:
                case LandingConstants.ACTION_GOOGLE_LOGIN_VALIDATE:
                case LandingConstants.ACTION_REGISTER_VALIDATE:

                default:
                    this.Show();
                    break;
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (_messenger != null)
                return;
            _messenger = Mvx.Resolve<IMvxMessenger>(); 
            _authService = Mvx.Resolve<IAuthenticationService>();
            _regService = Mvx.Resolve<IRegistrationService>();
        }
    }
}
