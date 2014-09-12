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
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Whistle.Core.Helper;
    using Whistle.Core.Helpers;
    using Whistle.Core.Modal;
    using Whistle.Core.Services;

    /// <summary>
    /// Define the MainViewModel type.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
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


        

        private void onUserAction(string value)
        {
            switch (value)
            {
                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    /*do some backend call ????.*/
                    if (WhistleEditViewModel.IsValid())
                    {
                        onCreateWhistle();
                    }
                    else                    
                        _messenger.Publish(new HomeMessage(this, HomeConstants.RESULT_WHISTLE_VALIDATION_FAILED));
                    break;
                case HomeConstants.NAV_WHISTLE_DISPLAY:
                    Settings.ShowWhistlersListMap = !Settings.ShowWhistlersListMap;
                    _messenger.Publish(new HomeMessage(this, HomeConstants.ACTION_SHOW_WHISTLERS));
                    break;
                case LandingConstants.ACTION_REGISTER_DONE:
                    //testing
                    if (!NewUser.IsValid())
                    {
                        _messenger.Publish(new HomeMessage(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED));
                        NewUser = new User();
                        return;
                    }
                    onRegister("PUT");
                    break;
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    if (!NewUser.IsValid())
                        _messenger.Publish(new HomeMessage(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED));
                    else
                        _messenger.Publish(new HomeMessage(this, value));
                    break;
                default:
                    _messenger.Publish(new HomeMessage(this, value));
                    break;
            }
        }

        protected async void onCreateWhistle()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<CreateWhistleRequest, CreateWhistleResponse>(
               
                new CreateWhistleRequest { Whistle = WhistleEditViewModel.GetNewWhistle() }, 
                ApiAction.CREATE_WHISTLE);
            IsBusy = false;
            if (result.HasError)
            {
                _messenger.Publish(new HomeMessage(this, HomeConstants.RESULT_WHISTLE_CREATION_FAILED).WithPayload(result.Error.Msg));
                return;
            }

            _messenger.Publish(new HomeMessage(this, HomeConstants.ACTION_SHOW_WHISTLERS));
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey(Settings.AccessTokenKey))
            {
                var userJson = parameters.Data[Settings.AccessTokenKey];
                NewUser = JsonConvert.DeserializeObject<User>(userJson);
                Settings.AccessToken = NewUser.AccessToken;
                // etc...
                Mvx.Trace(MvxTraceLevel.Diagnostic, "InitFromBundle MainViewModel with access token {0}", NewUser.AccessToken);
            }
        }

        public void SignOut()
        {
            Settings.AccessToken = string.Empty;
            this.ShowViewModel<LandingViewModel>();
        }
    }
}
