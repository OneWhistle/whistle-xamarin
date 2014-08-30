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
        internal static readonly string[] ActionList = new[]
        {
            ACTION_LOGIN_VALIDATE,
            ACTION_FORGOT_PASSWORD,
            ACTION_REGISTER_CONTINUE,
            ACTION_REGISTER_DONE,
            ACTION_REGISTER,
            ACTION_SIGNIN,
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
        public string UserAction { get; private set; }
    }
}
