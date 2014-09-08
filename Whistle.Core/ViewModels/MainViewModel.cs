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
    using System.Collections.ObjectModel;
    using Whistle.Core.Modal;

    /// <summary>
    /// Define the MainViewModel type.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private fields
        IMvxMessenger _messenger;
        string[] packageList = new[] { "ENVELOPS", "SMALL (UP TO 10 KG)", "MEDIUM (BETWEEN 11 - 50 KG)", "LARGE (BETWEEN 51 - 100 KG)", "EXTRA LARGE (MORE THAN 100 KG)" };
        string[] rideList = new[] { "BIKE(2 SEATS)", "AUTO(3 SEATS)", "SMALL CAR(4 SEATS)", "LARGE CAR(6 SEATS)", "MINI BUS(12 SEATS)", "BUS(20+ SEATS)", "TRUCK(ONLY PACKAGE)", "TRAIN", "FLIGHT" };
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


        private ObservableCollection<USettings> _uSetting;
        public ObservableCollection<USettings> USettings
        {
            get { return _uSetting; }
            set
            {
                _uSetting = value; RaisePropertyChanged(() => USettings);
            }
        }

        public string[] PackageList { get { return packageList; } }
        public string[] RideList { get { return rideList; } }


        private MvxCommand<string> selectUserType;
        public ICommand SelectUserType { get { return this.selectUserType ?? (this.selectUserType = new MvxCommand<string>(onUserTypeSelected)); } }

        private MvxCommand<string> navDisplay;
        public ICommand NavDisplay { get { return this.navDisplay ?? (this.navDisplay = new MvxCommand<string>(onNavDisplay)); } }

        private MvxCommand<string> userAction;
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand<string>(onUserAction)); } }



        private void onNavDisplay(string list)
        {
            var msg = new HomeMessage(this, HomeConstants.NAV_DISPLAY_LIST);
            msg.Parameter = list;
            _messenger.Publish(msg);
        }

        private void onUserTypeSelected(string value)
        {
            /*Update settings here..
            Settings.UserType..*/
            _messenger.Publish(new HomeMessage(this, HomeConstants.NAV_USER_TYPE_SELECTED));
        }

        private void onUserAction(string value)
        {
            switch (value)
            {
                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    /*do some backend call ????.*/
                    _messenger.Publish(new HomeMessage(this, value));
                    break;
                default:
                    _messenger.Publish(new HomeMessage(this, value));
                    break;
            }
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
