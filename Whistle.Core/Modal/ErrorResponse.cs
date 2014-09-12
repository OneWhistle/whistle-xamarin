using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public string Message { get; set; }

        public string GetErrorMessage()
        {
            if (string.IsNullOrEmpty(Message))
                return Msg;
            return Message;
        }
    }
}
