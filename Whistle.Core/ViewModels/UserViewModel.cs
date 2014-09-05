﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }

        public bool IsValid()
        {
            /*It should be better that that..*/
            if (string.IsNullOrEmpty(Password))
                return false;
            if (string.IsNullOrEmpty(Email))
                return false;
            if (string.IsNullOrEmpty(UserName))
                return false;
            if (string.IsNullOrEmpty(FullName))
                return false;
            return true;
        }
    }
}
