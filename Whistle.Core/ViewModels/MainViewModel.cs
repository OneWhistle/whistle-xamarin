// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Core.ViewModels
{
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.CrossCore;
    using Whistle.Core.Helpers;
    using System;

    /// <summary>
    /// Define the MainViewModel type.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private fields
        IMvxMessenger _messenger;

        #endregion

        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string _userName = "GEORGE";

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        public string UserName
        {
            get { return this._userName; }
        }


        private MvxCommand<string> selectUserType;
        public ICommand SelectUserType { get { return this.selectUserType ?? (this.selectUserType = new MvxCommand<string>(onUserTypeSelected)); } }

        private void onUserTypeSelected(string value)
        {
            /*Update settings here..
            Settings.UserType..*/
            _messenger.Publish(new HomeMessage(this, HomeConstants.NAV_USER_TYPE_SELECTED));
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (_messenger != null)
                return;
            _messenger = Mvx.Resolve<IMvxMessenger>();
        }
    }
}
