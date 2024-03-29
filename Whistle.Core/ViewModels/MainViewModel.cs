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
    using GeoJSON.Net.Geometry;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
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
        private IPhoneService phoneService;

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        public string UserName
        {
            get { return this.NewUser.UserName; }
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

        private MvxCommand<WhistleItemViewModel> displayWhistleItem;
        public ICommand DisplayWhistleItem { get { return this.displayWhistleItem ?? (this.displayWhistleItem = new MvxCommand<WhistleItemViewModel>(onDisplayWhistleItem)); } }

        private MvxCommand selectUserType;
        public ICommand SelectUserType { get { return this.selectUserType ?? (this.selectUserType = new MvxCommand(onUserTypeSelected)); } }

        private MvxCommand<string> navDisplay;
        public ICommand NavDisplay { get { return this.navDisplay ?? (this.navDisplay = new MvxCommand<string>(onNavDisplay)); } }

        private MvxCommand<string> userAction;
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand<string>(onUserAction)); } }

        public MvxCommand contactWhistler;
        public ICommand ContactWhistler { get { return this.contactWhistler ?? (this.contactWhistler = new MvxCommand(onContactWhistler)); } }


        public WhistleItemViewModel SelectedWhistleItem { get; private set; }

        public ContextSwitchViewModel ContextSwitchViewModel { get; private set; }

        //public WhistleEditViewModel WhistleEditViewModel { get; private set; }

        public WhistleResultViewModel WhistleResultViewModel { get; private set; }

        public ContactWhistlerViewModel ContactWhistlerViewModel { get; private set; }

        public TrackingViewModel TrackingViewModel { get; private set; }

        public MainViewModel(IMvxMessenger messenger)
        //IMvxLocationWatcher locationWatcher)
        {
            _messenger = messenger;
           // WhistleEditViewModel = new WhistleEditViewModel();
            ContextSwitchViewModel = new ContextSwitchViewModel();
            WhistleResultViewModel = new WhistleResultViewModel(new MatchingWhistle[] { });

        }


        //private bool canContactWhistler()
        //{
        //    return SelectedWhistleItem != null;
        //}

        private void onDisplayWhistleItem(WhistleItemViewModel whistleItem)
        {
            //this.WhistleEditViewModel = new WhistleEditViewModel
            //{

            //};

            this.WhistleResultViewModel = new ViewModels.WhistleResultViewModel(whistleItem.Whistle.MatchingWhisltes);
            _messenger.Publish(new MessageHandler(this, HomeConstants.ACTION_SHOW_WHISTLERS));
        }

        private void onContactWhistler()
        {
            if (SelectedWhistleItem == null)
                return;
            this.ContactWhistlerViewModel = new ContactWhistlerViewModel();
            _messenger.Publish(new MessageHandler(this, HomeConstants.NAV_CONTACT_WHISTLER));
        }

        private void onNavDisplay(string list)
        {
            var msg = new MessageHandler(this, HomeConstants.NAV_DISPLAY_LIST);
            msg.Parameter = list;
            _messenger.Publish(msg);
        }

        private void onUserTypeSelected()
        {
            var msg = new MessageHandler(this, HomeConstants.NAV_USER_TYPE_SELECTED);
            if (ContextSwitchViewModel.IsConsumerChecked)
            {
                if (Settings.UserType != 0)
                {
                    msg.WithPayload("notimportant");
                    Settings.UserType = 0;
                }
                Mvx.Trace(MvxTraceLevel.Diagnostic, "onUserTypeSelected CONSUMER");
            }
            if (ContextSwitchViewModel.IsProviderChecked)
            {
                if (Settings.UserType != 1)
                {
                    msg.WithPayload("notimportant");
                    Settings.UserType = 1;
                } Mvx.Trace(MvxTraceLevel.Diagnostic, "onUserTypeSelected PROVIDER");
            }
            if (ContextSwitchViewModel.IsTrackingChecked)
            {
                if (Settings.UserType != 2)
                {
                    msg.WithPayload("notimportant");
                    Settings.UserType = 2;
                }
                Mvx.Trace(MvxTraceLevel.Diagnostic, "onUserTypeSelected TRACKING");
            }
            /*Update settings here..
            Settings.UserType..*/
            _messenger.Publish(msg);
        }

        public void UpdateUserLocation(double longitude, double latitude)
        {
            var location = new CustomLocation(longitude, latitude);
            Task.Factory.StartNew(() =>
                {
                    innerUpdateUserLocation(location);
                });
        }

        private async void innerUpdateUserLocation(CustomLocation location)
        {
            var result = await ServiceHandler.PostAction<dynamic, User>(new { user = new User { Location = location } }, ApiAction.UPDATE_PROFILE, "PUT");

            if (result.HasError)
                return;
        }

        private void onUserAction(string value)
        {
            switch (value)
            {
                case HomeConstants.ACTION_SHOW_WHISTLERS:
                    /*do some backend call ????.*/
                    {
                        var err = WhistleEditViewModel.IsValid();
                        if (err.Length == 0)
                        {
                            onCreateWhistle();
                        }
                        else
                            _messenger.Publish(new MessageHandler(this, HomeConstants.RESULT_WHISTLE_VALIDATION_FAILED).WithPayload(err[0]));
                    }
                    break;
                case HomeConstants.NAV_WHISTLE_DISPLAY:
                    Settings.ShowWhistlersListMap = !Settings.ShowWhistlersListMap;
                    _messenger.Publish(new MessageHandler(this, HomeConstants.ACTION_SHOW_WHISTLERS));
                    break;
                case LandingConstants.ACTION_REGISTER_DONE:
                    {
                        var err = NewUser.IsValid(phoneService);
                        if (err.Length > 0)
                        {
                            _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED).WithPayload(err[0]));
                            return;
                        }
                        onUserUpdate("PUT");
                    }
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                    _messenger.Publish(new MessageHandler(this, value));
                    break;
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    {
                        var err = NewUser.IsValid(phoneService);
                        if (err.Length > 0)
                            _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED).WithPayload(err[0]));
                        else
                            _messenger.Publish(new MessageHandler(this, value));
                    }
                    break;

                case HomeConstants.NAV_CLEAR_SELECTED_WHISTLER:
                    SelectedWhistleItem = null;
                    RaisePropertyChanged(() => SelectedWhistleItem);
                    break;
                default:
                    _messenger.Publish(new MessageHandler(this, value));
                    break;
            }
        }

        protected async void onCreateWhistle()
        {
            IsBusy = true;
            var whistle = WhistleEditViewModel.GetNewWhistle();
            whistle.Provider = ContextSwitchViewModel.IsProviderChecked;
            var result = await ServiceHandler.PostAction<CreateWhistleRequest, CreateWhistleResponse>(
                new CreateWhistleRequest { Whistle = whistle },
                ApiAction.CREATE_WHISTLE);
            IsBusy = false;

            if (result.HasError)
            {
                _messenger.Publish(new MessageHandler(this, HomeConstants.RESULT_WHISTLE_CREATION_FAILED).WithPayload(result.Error.GetErrorMessage()));

                //THE following code will be removed.
                var lat = WhistleEditViewModel.SourcePoint.Coordinates[0];
                var lng = WhistleEditViewModel.SourcePoint.Coordinates[1];

                result = new ServiceResult<CreateWhistleResponse>(new CreateWhistleResponse
                {
                    NewWhistle = new Whistle
                    {
                       
                    },
                    MatchingWhisltes = new[]                    
                        {                                    
                          new MatchingWhistle{ Dis = 0, Obj  = new User { Location= new CustomLocation{ Coordinates = new []{lat+0.002,lng-0.001}}}},                   
                           new MatchingWhistle{ Dis = 0, Obj  = new User { Location= new CustomLocation{ Coordinates = new []{lat-0.001,lng+0.002}}}},                    
                        }
                }, "will_mock_asap");

            }

            SelectedWhistleItem = null;
            this.WhistleResultViewModel = new ViewModels.WhistleResultViewModel(result.Result.MatchingWhisltes);
            _messenger.Publish(new MessageHandler(this, HomeConstants.ACTION_SHOW_WHISTLERS));
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);            

            if (parameters.Data.ContainsKey(Settings.AccessTokenKey))
            {
                var userJson = parameters.Data[Settings.AccessTokenKey];
                NewUser = JsonConvert.DeserializeObject<User>(userJson);
                var userWhistles = JsonConvert.DeserializeObject<UserWhistle>(userJson);
                Settings.AccessToken = NewUser.AccessToken;
                // etc...
                Mvx.Trace(MvxTraceLevel.Diagnostic, "InitFromBundle MainViewModel with access token {0}", NewUser.AccessToken);
                this.TrackingViewModel = new ViewModels.TrackingViewModel(userWhistles.Whistles);
            }
            phoneService = Mvx.Resolve<IPhoneService>();
        }


        public void SignOut()
        {
            Settings.AccessToken = string.Empty;
            this.ShowViewModel<LandingViewModel>();
        }

        protected override void afterUserUpdate(string json)
        {

        }

        public void ShowWhistler()
        {
            SelectedWhistleItem = new WhistleItemViewModel(new Whistle());// change by whistlerItem
            RaisePropertyChanged(() => SelectedWhistleItem);
        }
    }
}
