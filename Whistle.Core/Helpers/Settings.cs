// Helpers/Settings.cs
using Cirrious.CrossCore;
using Refractored.MvxPlugins.Settings;

namespace Whistle.Core.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Simply setup your settings once when it is initialized.
        /// </summary>
        private static ISettings m_Settings;
        private static ISettings AppSettings
        {
            get
            {
                return m_Settings ?? (m_Settings = Mvx.GetSingleton<ISettings>());
            }
        }

#region Setting Constants

        private const string UserNameKey = "user_name";
        private static string UserNameDefault = string.Empty;


        private const string AccessTokenKey = "access_token";
        private static string AccessTokenDefault = string.Empty;

#endregion

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);
            }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(AccessTokenKey, value))
                    AppSettings.Save();
            }
        }

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(UserNameKey, value))
                    AppSettings.Save();
            }
        }
    }
}