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
using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace Whistle.Droid.Fragments
{
    public class LoginFragments : MvxFragment
    {

        public event Action<int> SignUpButtonClick = delegate { };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }

        #region OnCreateView Method

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
          // return inflater.Inflate(Resource.Layout.Login, container, false);
           var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view =this.BindingInflate(Resource.Layout.Login, null);
            var registrationBotton = view.FindViewById<Button>(Resource.Id.btnRegister);
            registrationBotton.Tag = 0;
            registrationBotton.Click += registrationBotton_Click;
            return view;
        }

        void registrationBotton_Click(object sender, EventArgs e)
        {
            SignUpButtonClick((int) ((Button) sender).Tag);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }

        #endregion
    }
}