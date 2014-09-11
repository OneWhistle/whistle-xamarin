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
    using Cirrious.MvvmCross.Plugins.PictureChooser;
    using System.IO;
    using System.Collections.ObjectModel;
    using Whistle.Core.Helpers;
    using Newtonsoft.Json;
    using Cirrious.CrossCore.Platform;

    /// <summary>
    /// Define the LandingViewModel type.
    /// </summary>
    public class LandingViewModel : BaseViewModel
    {
        #region Private fields
        IMvxMessenger _messenger;

        #endregion

        #region Private Picture Properties

        private readonly IMvxPictureChooserTask _pictureChooserTask;

        #endregion

        #region Constructor

        public LandingViewModel(IMvxPictureChooserTask pictureChooserTask)
        {
            _pictureChooserTask = pictureChooserTask;
        }

        #endregion

        #region Properties

        private User newUser;
        public User NewUser
        {
            get { return newUser; }
            private set
            {
                newUser = value; RaisePropertyChanged("NewUser");
            }
        }


        #endregion

        #region User Action
        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand<bool> updateGender;
        /// <summary>
        /// Gets user action command.
        /// </para>
        /// </summary>
        public ICommand UpdateGender { get { return this.updateGender ?? (this.updateGender = new MvxCommand<bool>(OnUpdateGender)); } }

        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand<string> userAction;
        /// <summary>
        /// Gets user action command.
        /// </para>
        /// </summary>
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand<string>(OnUserAction)); } }

        #endregion

        #region Picture Commands

        private void TakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPictureTaking, () => { });
        }

        private void ChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPictureTaking, () => { Mvx.Trace("Picture cancelled by user!"); });
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set { _imageBytes = value; RaisePropertyChanged(() => ImageBytes); }
        }


        private void OnPictureTaking(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);
            ImageBytes = memoryStream.ToArray();
        }

        #endregion

        #region Show MainViewModel


        #endregion

        #region User Action Implementation


        private void OnUpdateGender(bool arg)
        {
            this.NewUser.IsMale = arg;
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
                        _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED));
                        NewUser = new User();
                        return;
                    }
                    onRegister();
                    break;
                case LandingConstants.ACTION_FB_LOGIN_VALIDATE:
                case LandingConstants.ACTION_TWITTER_LOGIN_VALIDATE:
                case LandingConstants.ACTION_GOOGLE_LOGIN_VALIDATE:
                    this.ShowViewModel<MainViewModel>(new MvxBundle());
                    break;                
                case LandingConstants.ACTION_TAKE_PICTURE_CAMERA:
                    TakePicture();
                    break;
                case LandingConstants.ACTION_TAKE_PICTURE_GALLERY:
                    ChoosePicture();
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                case LandingConstants.ACTION_REGISTER:
                    IsUpadetMode = false;
                    _messenger.Publish(new LandingMessage(this, action));
                    break;
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    if (!NewUser.IsValid())
                        _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED));
                    else
                        _messenger.Publish(new LandingMessage(this, action));
                    break;
                default:
                    _messenger.Publish(new LandingMessage(this, action));
                    break;
            }
        }

        #endregion

        #region Init Bundle

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            this.NewUser = new User();
            if (_messenger != null)
                return;
            _messenger = Mvx.Resolve<IMvxMessenger>();
        }

        #endregion

        #region User Log In

        protected async void onLogin()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<User, User>( NewUser , ApiAction.LOGIN);
            IsBusy = false;
            if (result.HasError)
            {
                _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_BACKEND_ERROR).WithPayload(result.Error.Msg));
                NewUser = new User();
                return;
            }
            else
            {
                var bundle = new MvxBundle();
                bundle.Data.Add(Settings.AccessTokenKey, JsonConvert.SerializeObject(result.Result));
                this.ShowViewModel<MainViewModel>(bundle);
            }
        }


        #endregion

        #region User Registration

        protected async void onRegister()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<RegistrationRequest, RegistrationResponse>(new RegistrationRequest { User = newUser }, ApiAction.REGISTRATION);
            IsBusy = false;

            if (result.HasError)
            {
                _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_BACKEND_ERROR).WithPayload(result.Error.Msg));
            }
            else
            {
                Mvx.Trace(MvxTraceLevel.Diagnostic, "onRegister Success");
                _messenger.Publish(new LandingMessage(this, LandingConstants.RESULT_REGISTER_SUCCESS));
                await System.Threading.Tasks.Task.Delay(1500);
                var bundle = new MvxBundle();
                bundle.Data.Add(Settings.AccessTokenKey, JsonConvert.SerializeObject(result.Result.NewUser));
                this.ShowViewModel<MainViewModel>(bundle);
            }
            NewUser = new User();
        }

        #endregion
    }
}
