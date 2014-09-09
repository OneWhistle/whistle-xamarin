using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistle.Core.Modal;

namespace Whistle.Core.ViewModels
{
    public static class UserExtensions
    {

        public static bool IsValid(this User user)
        {
            /*It should be better that that..*/
            if (string.IsNullOrEmpty(user.Password))
                return false;
            //if (string.IsNullOrEmpty(user.Email))
            //    return false;
            if (string.IsNullOrEmpty(user.Name))
                return false;
            if (string.IsNullOrEmpty(user.UserName))
                return false;
            if (string.IsNullOrEmpty(user.DOB))
                return false;
            if (string.IsNullOrEmpty(user.Phone))
                return false;

            return true;
        }
    }
    
}
