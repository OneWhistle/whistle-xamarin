using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Whistle.Droid.Fragments
{
    public class RegistrationFragment : MvxFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RetainInstance = true;
        }

        #region OnCreateView

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Registration, container, false);
        }

        #endregion
    }
}