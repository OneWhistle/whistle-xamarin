// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Core.ViewModels
{
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using System.Linq;
    using System;

    /// <summary>
    /// Define the LandingViewModel type.
    /// </summary>
    public class LandingViewModel : BaseViewModel
    {


        #region Properties

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged("UserName"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged("Password"); }
        }
        #endregion

        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand<string> userAction;

        /// <summary>
        /// Gets My Command.
        /// <para>
        /// An example of a command and how to navigate to another view model
        /// Note the ViewModel inside of ShowViewModel needs to change!
        /// </para>
        /// </summary>
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand <string>(onUserAction)); } }

        private ICommand checkLogin;

        public ICommand CheckLogin
        {
            get { return this.checkLogin ?? (this.checkLogin = new MvxCommand(this.Show)); }
        }

        /// <summary>
        /// Show the view model.
        /// </summary>
        public void Show()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                this.ShowViewModel<MainViewModel>();
        }

        private void onUserAction(string action)
        {
            var messenger = Mvx.Resolve<IMvxMessenger>();
            if (!LandingConstants.ActionList.Contains(action))
                throw new InvalidOperationException();
            messenger.Publish(new LandingMessage(this, action));
        }
    }
}
