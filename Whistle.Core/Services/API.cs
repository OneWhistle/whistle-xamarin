﻿
namespace Whistle.Core.Services
{
    internal class Constant
    {

        //For future : If URL change then we'll change only here

        //For test
        internal const string UATWebUrl= "http://whistle-test.herokuapp.com/api/";
        //inter0nal const string LiveWebUrl = "";
    }

    internal class ApiSection
    {
        internal const string User = "user";
        internal const string Consumer = "consumer/";
        //So on...
    }
    internal class ApiAction
    {// too many thing...
        internal const string LOGIN = "user/login";
        internal const string UPDATE_PROFILE = "user";
        internal const string REGISTRATION = "user";
        internal const string CREATE_WHISTLE = "whistle";
        internal const string PASSWORD_RESET = "user/passwordReset";
        internal const string PASSWORD_RESET_REQUSET = "user/passwordResetReq";
        //So on...
    }

    public static class API
    {
        public static string CreateUrl(string apiSection)
        {
            return string.Format("{0}{1}", Constant.UATWebUrl, apiSection);
        }
    }
}
