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
using Whistle.Droid.Adepters;
using Fragment = Android.Support.V4.App.Fragment;

namespace Whistle.Droid.Fragments
{
    public class MenuFragments : Fragment
    {
        #region Private Declarations

        private LinearLayout WhistleMenu, ProfileMenu, FavoriteMenu,SettingMenu,AboutMenu,WhistleWorkMenu,LogoutMenu;

        public event Action<int> SelectedMenu = delegate { };

        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
            // Create your fragment here
        }

        #region OnCreateView

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Menu, container, false);

            #region Finding Menu View and Init their Click Event

            WhistleMenu = view.FindViewById<LinearLayout>(Resource.Id.WhistleMenu);
            WhistleMenu.Tag = 1;
            WhistleMenu.Click += MenuItem_Click;

            ProfileMenu = view.FindViewById<LinearLayout>(Resource.Id.ProfileMenu);
            ProfileMenu.Tag = 2;
            ProfileMenu.Click += MenuItem_Click;

            FavoriteMenu = view.FindViewById<LinearLayout>(Resource.Id.FavoriteMenu);
            FavoriteMenu.Tag = 3;
            FavoriteMenu.Click += MenuItem_Click;

            SettingMenu = view.FindViewById<LinearLayout>(Resource.Id.SettingMenu);
            SettingMenu.Tag = 4;
            SettingMenu.Click += MenuItem_Click1;

            AboutMenu = view.FindViewById<LinearLayout>(Resource.Id.AboutMenu);
            AboutMenu.Tag = 5;
            AboutMenu.Click += MenuItem_Click;

            WhistleWorkMenu = view.FindViewById<LinearLayout>(Resource.Id.WhistleWorkMenu);
            WhistleWorkMenu.Tag = 6;
            WhistleWorkMenu.Click += MenuItem_Click;

            LogoutMenu = view.FindViewById<LinearLayout>(Resource.Id.LogoutMenu);
            LogoutMenu.Tag = 7;
            LogoutMenu.Click += MenuItem_Click;

            #endregion

            return view;
        }

        void MenuItem_Click(object sender, EventArgs e)
        {
            var targetMenu = (LinearLayout)sender;
            SelectedMenu((int)targetMenu.Tag);
        }
        void MenuItem_Click1(object sender, EventArgs e)
        {
            var targetMenu = (LinearLayout)sender;
            SelectedMenu((int)targetMenu.Tag);
        }
        #endregion

        #region OnViewCreated

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
        }

        #endregion
    }
}