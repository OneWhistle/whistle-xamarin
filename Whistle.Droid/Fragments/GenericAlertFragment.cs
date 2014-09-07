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
using Whistle.Core.Helpers;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Whistle.Droid.Views;

namespace Whistle.Droid.Fragments
{
    public class GenericAlertFragment : GenericDialogFragment
    {
        int _iconResId;
        int _titleResId;
        Func<string> _descriptionResolve;

        public bool HasIcon { get; private set; }

        public bool HasTitle { get; private set; }

        public bool HasDescription { get; private set; }

        public GenericAlertFragment(int backgroundResId) : base(Resource.Layout.AlertDialog, backgroundResId) { }


        public GenericAlertFragment WithIcon(int iconResId)
        {
            HasIcon = true;
            _iconResId = iconResId;
            return this;
        }

        public GenericAlertFragment WithTitle(int textResId)
        {
            HasTitle = true;
            _titleResId = textResId;
            return this;
        }

        public GenericAlertFragment WithDescription(int msgResId)
        {
            HasDescription = true;
            _descriptionResolve = () => GetString(msgResId); // I use a delegate (function) because calling GetString Here will throw IllegalStateException
            return this;
        }
        public GenericAlertFragment WithDescription(string description)
        {
            HasDescription = true;
            _descriptionResolve = ()=> description;
            return this;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var titleView = view.FindViewById<TextView>(Resource.Id.alertTitle);
            var descView = view.FindViewById<TextView>(Resource.Id.alertMsg);
            var iconView = view.FindViewById<ImageView>(Resource.Id.alertIcon);

            if (HasIcon)
                iconView.SetImageResource(_iconResId);
            else
                iconView.Visibility = ViewStates.Gone;
            if (HasTitle)
                titleView.Text = GetString(_titleResId);
            else
                titleView.Visibility = ViewStates.Gone;
            if (HasDescription)
            {
                descView.Text = _descriptionResolve(); // The description is resolved when needed.
            }
            else
                descView.Visibility = ViewStates.Gone;

            return view;
        }



        View AddButton(LinearLayout _view, IList<string> _buttons)
        {
            //For Two butotn LO wieghtSum = 2; weight="1.2" button:layout_weight="1"
            //For Single Button Button:layout_weight="1.2"
            _view.WeightSum = 2;
            foreach (var button in _buttons)
            {
                var newButton = new WhistleButton(_view.Context);
                newButton.Text = button;
                _view.AddView(newButton);
            }
            return _view;
        }
    }

    public class AlertHelper
    {
        public int LayoutID { get; set; }
        public int IconID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> AlertButton { get; set; }
        //Please suggest color info
    }
}