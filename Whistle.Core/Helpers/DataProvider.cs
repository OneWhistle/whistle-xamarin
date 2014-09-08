using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistle.Core.Modal;

namespace Whistle.Core
{
    public class DataProvider
    {
        public DataProvider()
        {
        }
        private static ObservableCollection<USettings> _settingItems;

        public static ObservableCollection<USettings> SettingItems(int[] _icons)
        {

            //We'll use SQLite database to store settings and Its state
            _settingItems = new ObservableCollection<USettings>() { new USettings { Title = "Notification", Description = "Turn On/Off Jingle, Vibrate & More...", MenuIcon=_icons[0]},
                                                 new USettings { Title = "Account", Description = "Manage your whistle account", MenuIcon=_icons[1]},
                                                 new USettings { Title = "Preferences", Description = "Set Radius, Location, Fare & More...", MenuIcon=_icons[2]},
                                                 new USettings { Title = "Privacy", Description = "Control Block List, Status & More", MenuIcon=_icons[3]},
                                                 new USettings { Title = "Notification", Description = "FAQ'S, Legal, Contact Us & More...", MenuIcon=_icons[4]}
            };
            return _settingItems;
        }
    }
}
