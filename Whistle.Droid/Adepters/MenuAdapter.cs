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

        public MenuAdapter(Context _context, List<Menus> _items)
        {
            this.context = _context;
            this.items = _items;
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
            view.FindViewById<TextView>(Resource.Id.Badge).Text = Convert.ToString(menuItem.Badges);

            var mailImage = view.FindViewById<ImageView>(Resource.Id.menuIcon);

            return view;
        }

        #endregion

        public override Menus this[int position]
        {
            get { return items[position]; }
        }
    }
}