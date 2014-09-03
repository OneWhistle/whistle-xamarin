using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class Users
    {
        //public int  UserID { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string cnfmPassword { get; set; }
        public DateTime? dob { get; set; }
        public string phone { get; set; }
    }
}
