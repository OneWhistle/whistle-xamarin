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
    public class GenericAlertFragment : MvxDialogFragment
    {
        private AlertHelper alertHelper;
        private TextView txtTitleView, txtDescription;
        private ImageView imageIcon;
        private WhistleButton alertButton;

        public GenericAlertFragment(AlertHelper _alertHelper)
        {
            alertHelper = _alertHelper;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = this.BindingInflate(alertHelper.LayoutID, null);
            return view;
        }


        public override Dialog OnCreateDialog(Bundle savedState)
        {
            base.EnsureBindingContextSet(savedState);
            var view = this.BindingInflate(alertHelper.LayoutID, null);
            var linearLayout = view.FindViewById<LinearLayout>(Resource.Id.bottomLayout);
            AddButton(linearLayout, new List<string>() { "Find", "Resend" });


            var dialog = new Dialog(Activity);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            dialog.SetContentView(view);
            dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dialog.Window.SetBackgroundDrawableResource(Resource.Drawable.transparent);
            return dialog;
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