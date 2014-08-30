using Cirrious.MvvmCross.Plugins.Messenger;

namespace Whistle.Core
{
    public sealed class LandingConstants
    {
        public const string ACTION_REGISTER = "ACTION_REGISTER";
        public const string ACTION_SIGNIN = "ACTION_SIGNIN";

        internal static readonly string[] ActionList = new[]
        {
            ACTION_REGISTER,
            ACTION_SIGNIN
        };
    }

    public class LandingMessage: MvxMessage
    {
        public LandingMessage(object sender, string userAction):base(sender)
        {
            this.UserAction = userAction;
        }
        public string UserAction { get; private set; }
    }
}
