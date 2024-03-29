// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Whistle.Core.Modal;

namespace Whistle.Core.ViewModels
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.Plugins.PictureChooser;
    using Cirrious.MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using Whistle.Core.Helper;
    using Whistle.Core.Helpers;
    using Whistle.Core.Services;

    /// <summary>
    /// Define the LandingViewModel type.
    /// </summary>
    public class LandingViewModel : BaseViewModel
    {
        private IPhoneService phoneService;

        #region Private Picture Properties

        private readonly IMvxPictureChooserTask _pictureChooserTask;

        #endregion

        public override bool IsUserCreationMode { get { return true; } }
        private string _resetPasswordPhone;
        public string ResetPasswordPhone { get { return _resetPasswordPhone; } set { _resetPasswordPhone = value; RaisePropertyChanged(() => ResetPasswordPhone); } }

        private PasswordResponse _passwordResetResponse;
        public PasswordResponse PasswordResetResponse { get { return _passwordResetResponse; } set { _passwordResetResponse = value; RaisePropertyChanged(() => PasswordResetResponse); } }

        private PasswordReset _passwordReset;
        public PasswordReset PasswordResets { get { return _passwordReset; } set { _passwordReset = value; RaisePropertyChanged(() => PasswordResets); } }

        #region Constructor

        public LandingViewModel(IMvxPictureChooserTask pictureChooserTask)
        {
            _pictureChooserTask = pictureChooserTask;
        }

        #endregion

        #region User Action
        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand<string> userAction;
        /// <summary>
        /// Gets user action command.
        /// </para>
        /// </summary>
        public ICommand UserAction { get { return this.userAction ?? (this.userAction = new MvxCommand<string>(OnUserAction)); } }

        private MvxCommand<string> userSelection;
        /// <summary>
        /// Gets user action command.
        /// </para>
        /// </summary>
        public ICommand UserSelection { get { return this.userSelection ?? (this.userSelection = new MvxCommand<string>(OnUserSelection)); } }

        #endregion

        #region Picture Commands

        private void TakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
        }
       
        private void ChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { Mvx.Trace("Picture cancelled by user!"); });
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set { _imageBytes = value; RaisePropertyChanged(() => ImageBytes); }
        }


        private void OnPicture(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);
            ImageBytes = memoryStream.ToArray();
        }

        #endregion

        #region User Action Implementation

        private void OnUserAction(string action)
        {
            if (!LandingConstants.ActionList.Contains(action))
                throw new InvalidOperationException();
            switch (action)
            {
                case LandingConstants.ACTION_LOGIN_VALIDATE:
                    onLogin();
                    break;
                case LandingConstants.ACTION_REGISTER_DONE:
                    //testing
                    var errors = NewUser.IsValid(phoneService);
                    if (errors.Length > 0)
                    {
                        _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED).WithPayload(errors[0]));
                        this.NewUser = getNewUser();
                        return;
                    }
                    NewUser.Location = new CustomLocation(10, 10); // to avoid location update fails.
                    if (!string.IsNullOrEmpty(Settings.AccessToken))
                        onUserUpdate("PUT");
                    else
                        onUserUpdate();
                    break;
                case LandingConstants.ACTION_FB_LOGIN_VALIDATE:
                case LandingConstants.ACTION_TWITTER_LOGIN_VALIDATE:
                case LandingConstants.ACTION_GOOGLE_LOGIN_VALIDATE:

                    var bundle = new MvxBundle();
#if DEBUG
                    bundle.Data.Add(Settings.AccessTokenKey, @"{""__v"":9,""_id"":""5414b8b3f8e1d50b0016b0c1"",""accessToken"":""887393c64ca993c34efc5c095d64f6b6"",""dob"":""2014-08-13T00:00:00.000Z"",""email"":"""",""lastLogin"":""2014-09-15T06:12:08.773Z"",""name"":""andrei talantsy"",""password"":""talanta"",""phone"":""+33669081609"",""username"":""talanta3"",""Whistles"":[{""_id"":""5414bd88f8e1d50b0016b0c8"",""provider"":false,""public"":true,""comment"":""test"",""type"":""MINI_BUS"",""size"":[2,3],""connections"":[]},{""_id"":""5414be6ff8e1d50b0016b0c9"",""provider"":false,""public"":true,""comment"":""test"",""type"":""MINI_BUS"",""size"":[2,3],""connections"":[]},{""_id"":""5414c9e9e083c40b0089e327"",""provider"":false,""public"":true,""comment"":null,""type"":""MINI_BUS"",""size"":[3,4],""connections"":[]},{""_id"":""5414ca2fe083c40b0089e328"",""provider"":true,""public"":true,""comment"":null,""type"":""MINI_BUS"",""size"":[3,4],""connections"":[]},{""_id"":""5415884621fa1f0b0038d6f3"",""provider"":false,""public"":true,""comment"":null,""type"":""LARGE_CAR"",""size"":[3],""connections"":[]},{""_id"":""5415936d21fa1f0b0038d6f5"",""provider"":true,""public"":true,""comment"":null,""type"":""LARGE_CAR"",""size"":[3],""connections"":[]},{""_id"":""541596ae21fa1f0b0038d6f7"",""provider"":true,""public"":true,""comment"":null,""type"":""LARGE_CAR"",""size"":[3],""connections"":[]},{""_id"":""5415990921fa1f0b0038d6f9"",""provider"":true,""public"":true,""comment"":null,""type"":""LARGE_CAR"",""size"":[3],""connections"":[]},{""_id"":""54159a7921fa1f0b0038d6fb"",""provider"":false,""public"":true,""comment"":null,""type"":""LARGE_CAR"",""size"":[3],""connections"":[]}],""location"":{""type"":""Point"",""coordinates"":[65.9667,-18.5333]}}	");
#endif
                    this.ShowViewModel<MainViewModel>(bundle);
                    break;
                case LandingConstants.ACTION_TAKE_PICTURE_CAMERA:
                    TakePicture();
                    break;
                case LandingConstants.ACTION_TAKE_PICTURE_GALLERY:
                    ChoosePicture();
                    break;
                case LandingConstants.ACTION_REGISTER:
                    _messenger.Publish(new MessageHandler(this, action));
                    break;
                case LandingConstants.ACTION_REGISTER_CONTINUE:
                case LandingConstants.ACTION_REGISTER_VALIDATE:
                    var validationErr = NewUser.IsValid(phoneService);
                    if (validationErr.Length > 0)
                        _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED).WithPayload(validationErr[0]));
                    else
                        _messenger.Publish(new MessageHandler(this, action));
                    break;
                case LandingConstants.ACTION_GET_PASSWORD_RESET_KEY:
                    GetPasswordResetKey();
                    break;
                case LandingConstants.ACTION_RESET_PASSWORD:
                     var pErrors = PasswordResets.IsPassowrdMatched();
                     if (pErrors.Length > 0)
                    {
                        _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_REGISTER_VALIDATION_FAILED).WithPayload(pErrors[0]));
                        this.PasswordResets = new PasswordReset();
                        return;
                    }
                    ResetPassowrd();
                    break; 
                default:
                    _messenger.Publish(new MessageHandler(this, action));
                    break;
            }
        }

        #endregion

        #region Password Reset Key Grabber

        protected async void GetPasswordResetKey()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<PasswordResetRequest, PasswordResponse>(new PasswordResetRequest { Phone=ResetPasswordPhone }, ApiAction.PASSWORD_RESET_REQUSET);
            IsBusy = false;
            if (result.HasError)
            {
                _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_BACKEND_ERROR).WithPayload(result.Error.GetErrorMessage()));
                this.NewUser = getNewUser();
                return;
            }
            else
            {
                Mvx.Trace(MvxTraceLevel.Diagnostic, "GetPasswordKey Success");
                PasswordResetResponse = result.Result;
                _messenger.Publish(new MessageHandler(this, LandingConstants.ACTION_ENTER_CODE));
            }
        }


        protected async void ResetPassowrd()
        {
            IsBusy = true;
            PasswordResets.PasswordKey = PasswordResetResponse.Key; // We'll handle smartly
            PasswordResets.Phone = ResetPasswordPhone;
            var result = await ServiceHandler.PostAction<PasswordReset, PasswordResetResponse>(PasswordResets, ApiAction.PASSWORD_RESET);
            IsBusy = false;
            if (result.HasError)
            {
                _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_BACKEND_ERROR).WithPayload(result.Error.GetErrorMessage()));
                this.PasswordResets = new PasswordReset();
                return;
            }
            else
            {
                Mvx.Trace(MvxTraceLevel.Diagnostic, "ResetPassword Success");
                
                _messenger.Publish(new MessageHandler(this, LandingConstants.ACTION_SIGNIN));
               // _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_RESET_PASSWORD_SUCCESS));
            }
        }
        #endregion

        #region Init Bundle

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            phoneService = Mvx.Resolve<IPhoneService>();
            this.NewUser = getNewUser();
            PasswordResets = new PasswordReset();
        }

        #endregion

        #region User Log In

        protected async void onLogin()
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<User, User>(NewUser, ApiAction.LOGIN);

            IsBusy = false;
            if (result.HasError)
            {
                _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_BACKEND_ERROR).WithPayload(result.Error.GetErrorMessage()));
                this.NewUser = getNewUser();
                return;
            }
            else
            {
                var bundle = new MvxBundle();
                bundle.Data.Add(Settings.AccessTokenKey, result.RawJsonResponse);
                this.ShowViewModel<MainViewModel>(bundle);
            }
        }

        #endregion

        private void OnUserSelection(string selection)
        {
            var msg = new MessageHandler(this, LandingConstants.ACTION_USER_SELECTION);
            msg.Parameter = selection;
            _messenger.Publish(msg);
        }

        protected override void onUserUpdateFail()
        {
            base.onUserUpdateFail();
            this.NewUser = getNewUser();
        }

        protected async override void afterUserUpdate(string rawJson)
        {
            await System.Threading.Tasks.Task.Delay(1500);
            var jso = JObject.Parse(rawJson);
            var user = jso["newUser"].ToString() ;

            var bundle = new MvxBundle();
            bundle.Data.Add(Settings.AccessTokenKey, user);
            this.ShowViewModel<MainViewModel>(bundle);
            this.NewUser = getNewUser();
        }

        private User getNewUser()
        {
            var result = new User();
     //       result.Phone = phoneService.GetPhoneNumber();
            return result;
        }
    }
}
