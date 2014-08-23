using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Whistle.Core.Modal;

namespace Whistle.Droid.Adepters
{
    public class MenuAdapter : BaseAdapter<Menus>
    {
        #region Private Declarations

        Context context;
        List<Menus> items;

        #endregion

        #region Constructor

        public MenuAdapter(Context _context)
        {
            this.context = _context;
            this.items = new List<Menus>() { new Menus { Title = "WHISTLE", MenuIcon = Resource.Drawable.whistle_white_icon },
            new Menus { Title = "EDIT PROFILE", MenuIcon = Resource.Drawable.profile_green_icon},
            new Menus { Title = "MY FAVORITE", MenuIcon = Resource.Drawable.favorites_white_icon },
            new Menus { Title = "SETTING", MenuIcon = Resource.Drawable.settings_white_icon },
            new Menus { Title = "ABOUT WHISTLE", MenuIcon = Resource.Drawable.about_white_icon },
            new Menus { Title = "HOW WHISTLE WORK", MenuIcon = Resource.Drawable.howitworks_white_icon },
            new Menus { Title = "SIGN OUT", MenuIcon = Resource.Drawable.signout_white_icon }};
        }

        #endregion

        #region Get Item ID

        public override long GetItemId(int position)
        {
            return position;
        }

        #endregion

        #region Item Count

        public override int Count
        {
            get { return items.Count; }
        }

        #endregion

        #region GetItem Object

        public override Java.Lang.Object GetItem(int position)
        {
            return new Java.Lang.String(items[position].ToString());
        }

        #endregion

        #region Rendering View

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var menuItem = items[position];

            View view = convertView; // re-use an existing view, if one is available
            if (view == null)
            { // otherwise create a new one
                view = LayoutInflater.From(context).Inflate(Resource.Layout.MenuItem, parent, false);
            }

            view.FindViewById<TextView>(Resource.Id.Title).Text = menuItem.Title;
           // view.FindViewById<TextView>(Resource.Id.Badge).Text = Convert.ToString(menuItem.Badges);

            var menuIcon = view.FindViewById<ImageView>(Resource.Id.menuIcon);
            menuIcon.SetImageResource(menuItem.MenuIcon);
            return view;
        }

        #endregion

        public override Menus this[int position]
        {
            get { return items[position]; }
        }
    }
}