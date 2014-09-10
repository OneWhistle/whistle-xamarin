// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Core.ViewModels
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Plugins.Location;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.ViewModels;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Whistle.Core.Helpers;
    using Whistle.Core.Modal;

    /// <summary>
    /// Define the MainViewModel type.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private fields
        readonly IMvxMessenger _messenger;
        //readonly IMvxLocationWatcher _locationWatcher;

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

        #region Properties

        private ObservableCollection<string> favorites = new ObservableCollection<string>() { "test", "test", "test", "test" };
        public ObservableCollection<string> Favorites
        {
            get { return favorites; }
            set
            {
                favorites = value; RaisePropertyChanged(() => Favorites);
            }
        }


        #endregion

        public string[] PackageList { get { return packageList; } }
        public string[] RideList { get { return rideList; } }

        private MvxCommand<string> selectUserType;
        public ICommand SelectUserType { get { return this.selectUserType ?? (this.selectUserType = new MvxCommand<string>(onUserTypeSelected)); } }

        private MvxCommand<string> navDisplay;
        public ICommand NavDisplay { get { return this.navDisplay ?? (this.navDisplay = new MvxCommand<string>(onNavDisplay)); } }

        private MvxCommand<string> userAction;
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand<string>(onUserAction)); } }


        public WhistleEditViewModel WhistleEditViewModel { get; private set; }

        public MainViewModel(IMvxMessenger messenger)
        //IMvxLocationWatcher locationWatcher)
        {
            _messenger = messenger;
            WhistleEditViewModel = new WhistleEditViewModel();
        }


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


        #region Properties

        //TEMP testing
        private User newUser = new User { Email = "rzee.m7@gmail.com", IsMale = true, UserName = "rzee", Name = "M RIYAZ", Password = "IAm7MOM" };
        public User NewUser
        {
            get { return newUser; }
            private set
            {
                newUser = value; RaisePropertyChanged("NewUser");
            }
        }


        #endregion

        private void onUserAction(string value)
        {
            switch (value)
            {
                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    /*do some backend call ????.*/
                    if (WhistleEditViewModel.IsValid())
                        _messenger.Publish(new HomeMessage(this, value));
                    else
                        _messenger.Publish(new HomeMessage(this, HomeConstants.RESULT_WHISTLE_VALIDATION_FAILED));
                    break;
                case HomeConstants.NAV_WHISTLE_DISPLAY:
                    Settings.ShowWhistlersListMap = !Settings.ShowWhistlersListMap;
                    _messenger.Publish(new HomeMessage(this, HomeConstants.ACTION_SHOW_WHISTLERS));
                    break;
                default:
                    _messenger.Publish(new HomeMessage(this, value));
                    break;
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey(Settings.AccessTokenKey))
            {
                Settings.AccessToken = parameters.Data[Settings.AccessTokenKey];
                // etc...
            }
        }
    }
}
