using Cirrious.MvvmCross.Plugins.Messenger;

namespace Whistle.Core
{
    public sealed class HomeConstants
    {
        public const string NAV_USER_TYPE_SELECTED = "{FB91A325-8178-4D5A-AA1E-C9242C818257}";
        public const string NAV_DISPLAY_LIST = "{F498EEBA-976B-4646-855F-321C260FD424}";
        public const string NAV_WHISTLE_DISPLAY = "NAV_WHISTLE_DISPLAY";
        public const string ACTION_SHOW_WHISTLERS = "ACTION_SHOW_WHISTLERS";

        public const string RESULT_WHISTLE_VALIDATION_FAILED = "{8D805840-E932-4978-A564-581C821D4E05}";
        public const string RESULT_WHISTLE_CREATION_FAILED = "{{64BF466A-37E1-4E96-8378-5C00A95C0EB2}}";
    }

    public class HomeMessage : MvxMessage
    {
        //* It looks the same with landmessage
        // so i will create a base abstract class with the following
        // prototype.
        // I want to keep two separate types to avoid confusion
        
        public HomeMessage(object sender, string userAction)
            : base(sender)
        {
            this.UserAction = userAction;
        }
        public string UserAction { get; private set; }

        public string Parameter { get; internal set; }

        public bool HasPayload { get; private set; }
        public string Payload { get; private set; }


        internal HomeMessage WithPayload(string payload)
        {
            HasPayload = true;
            this.Payload = payload;
            return this;
        }
    }
}
