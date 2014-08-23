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
using Whistle.Droid.Fragments;

namespace Whistle.Droid
{
    [Activity]
    public class LandingActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Landing);

            //LoginButton > Login
            //registration > regi.
            var registrationButton = FindViewById<Button>(Resource.Id.btnRegister);
            registrationButton.Click += registrationButton_Click;
            var signInButton = FindViewById<Button>(Resource.Id.btnSignIn);
            signInButton.Click += registrationButton_Click;

            ActionBar.Hide();
        }

        void registrationButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MenuFragmentActivity));
           // OverridePendingTransition(Resource.Animation.enter, Resource.Animation.exit);
        }
    }
}