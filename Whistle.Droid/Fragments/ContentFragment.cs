using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace Whistle.Droid.Fragments
{
    public class ContentFragment : Fragment
    {
        private int _colorRes = -1;

        public ContentFragment() 
            : this(Resource.Color.red)
        { }

        public ContentFragment(int colorRes)
        {
            _colorRes = colorRes;
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (null != savedInstanceState)
                _colorRes = savedInstanceState.GetInt("_colorRes");
            var color = Resources.GetColor(_colorRes);
            var v = new RelativeLayout(Activity);
            v.SetBackgroundColor(color);
            return v;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("_colorRes", _colorRes);
        }
    }
}