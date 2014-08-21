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

namespace Whistle.Droid
{
    [Activity(MainLauncher = true)]
    public class LandingActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Landing);

            //LoginButton > Login
            //registration > regi.


            // Create your application here
        }
    }
}