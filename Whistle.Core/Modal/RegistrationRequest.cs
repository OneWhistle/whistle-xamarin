using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{

    public class RegistrationRequest
    {
        public User User { get; set; }
    }

    public class RegistrationResponse
    {
        public User NewUser { get; set; }
    }
}
