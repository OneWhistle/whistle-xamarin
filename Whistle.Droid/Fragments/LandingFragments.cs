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
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
//using Fragment = Android.Support.V4.App.Fragment;

namespace Whistle.Droid.Fragments
{
    public class LandingFragments : MvxFragment
    {
        /// <summary>
        /// Action to navigate frag.
        /// </summary>
        public event Action<int> LandingButtonClick = delegate { }; //ok :). 
        private Button registrationButton, signInButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }

        #region OnCreateView

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Landing, container, false);
            registrationButton = view.FindViewById<Button>(Resource.Id.btnRegister);
            registrationButton.Tag = 0;
            signInButton = view.FindViewById<Button>(Resource.Id.btnSignIn);
            signInButton.Tag = 1;
            signInButton.Click += Button_Click;
            registrationButton.Click += Button_Click;

            return view;
        }

        void Button_Click(object sender, EventArgs e)
        {
            LandingButtonClick((int)((Button)sender).Tag);
        }

        #endregion
    }
}