using Cirrious.MvvmCross.Plugins.Messenger;

namespace Whistle.Core
{
    public sealed class LandingConstants
    {
        public const string ACTION_REGISTER = "ACTION_REGISTER";
        public const string ACTION_SIGNIN = "ACTION_SIGNIN";
        public const string ACTION_FORGOT_PASSWORD = "ACTION_FORGOT_PASSWORD";
        public const string ACTION_REGISTER_CONTINUE = "ACTION_REGISTER_CONTINUE";
        public const string ACTION_REGISTER_DONE = "ACTION_REGISTER_DONE";
        public const string ACTION_LOGIN_VALIDATE = "ACTION_LOGIN_VALIDATE";
        public const string ACTION_FB_LOGIN_VALIDATE = "ACTION_FB_LOGIN_VALIDATE";
        public const string ACTION_TWITTER_LOGIN_VALIDATE = "ACTION_TWITTER_LOGIN_VALIDATE";
        public const string ACTION_GOOGLE_LOGIN_VALIDATE = "ACTION_GOOGLE_LOGIN_VALIDATE";
        public const string ACTION_REGISTER_VALIDATE = "ACTION_REGISTER_VALIDATE";
        public const string ACTION_PROFILE_IMAGE = "ACTION_PROFILE_IMAGE";
        public const string ACTION_GENDER_OPTION = "ACTION_GENDER_OPTION";
        public const string ACTION_DOB_OPTION = "ACTION_DOB_OPTION";
        public const string ACTION_TAKE_PICTURE_CAMERA = "ACTION_TAKE_PICTURE_CAMERA";
        public const string ACTION_TAKE_PICTURE_GALLERY = "ACTION_TAKE_PICTURE_GALLERY";


        public const string RESULT_LOGIN_FAILED = "RESULT_LOGIN_FAILED";
        public const string RESULT_REGISTER_SUCCESS = "RESULT_REGISTER_SUCCESS";
        public const string RESULT_REGISTER_VALIDATION_FAILED = "RESULT_REGISTER_VALIDATION_FAILED";

        internal static readonly string[] ActionList = new[]
        {
            ACTION_LOGIN_VALIDATE,
            ACTION_FORGOT_PASSWORD,
            ACTION_REGISTER_CONTINUE,
            ACTION_REGISTER_DONE,
            ACTION_REGISTER_VALIDATE,
            ACTION_REGISTER,
            ACTION_SIGNIN,
            ACTION_TAKE_PICTURE_GALLERY,
            ACTION_TAKE_PICTURE_CAMERA,
            ACTION_DOB_OPTION,
            ACTION_GENDER_OPTION,
            ACTION_PROFILE_IMAGE,
            ACTION_TWITTER_LOGIN_VALIDATE,
            ACTION_GOOGLE_LOGIN_VALIDATE,
            ACTION_FB_LOGIN_VALIDATE
        };
    }

    public class LandingMessage : MvxMessage
    {
        public LandingMessage(object sender, string userAction)
            : base(sender)
        {
            this.UserAction = userAction;
        }

        public bool HasPayload { get; private set; }
        public string Payload { get; private set; }

        internal LandingMessage WithPayload(string payload)
        {
            HasPayload = true;
            this.Payload = payload;
            return this;
        }

        public string UserAction { get; private set; }
    }
}
