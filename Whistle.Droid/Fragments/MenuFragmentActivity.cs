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
using SlidingMenuSharp;
using Fragment = Android.Support.V4.App.Fragment;

namespace Whistle.Droid.Fragments
{
    [Activity]
    [Obsolete("Please use MainView instead.")]
    public class MenuFragmentActivity : MainActivity
    {
        private Android.Support.V4.App.Fragment _content;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (null != savedInstanceState)
                _content = SupportFragmentManager.GetFragment(savedInstanceState, "_content");
            if (null == _content)
                _content = new ContentFragment(Resource.Color.red);

            SetContentView(Resource.Layout.Main);

            //
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.contentArea, _content)
                .Commit();

            SetBehindContentView(Resource.Layout.MenuHolder);
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.MenuFrame, new ContentMenuFragment())
                .Commit();

            SlidingMenu.TouchModeAbove = TouchMode.Fullscreen;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            SupportFragmentManager.PutFragment(outState, "_content", _content);
            
        }

        public void SwitchContent(Android.Support.V4.App.Fragment fragment)
        {
            _content = fragment;
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.contentArea, fragment)
                .Commit();
            SlidingMenu.ShowContent();

        }
    }
}