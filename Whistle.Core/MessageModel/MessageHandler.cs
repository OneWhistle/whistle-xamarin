using Cirrious.MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core
{
    //Confirm this I'll replace this at every where we are using Landing or Home Message
    public class MessageHandler : MvxMessage
    {
        public MessageHandler(object sender, string userAction)
            : base(sender)
        {
            this.UserAction = userAction;
        }

        public bool HasPayload { get; private set; }
        public string Payload { get; private set; }

        //took from HomeMessage
        public string Parameter { get; internal set; }

        internal MessageHandler WithPayload(string payload)
        {
            HasPayload = true;
            this.Payload = payload;
            return this;
        }

        public string UserAction { get; private set; }
    }
}
