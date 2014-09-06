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
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Whistle.Droid.Fragments
{
    /// <summary>
    /// https://github.com/MvvmCross/MvvmCross-Tutorials/blob/master/Fragments/FragmentSample.UI.Droid/Views/Frags/Dialog/NameDialogFragment.cs
    /// </summary>
    public class GenericDialogFragment : MvxDialogFragment
    {
        readonly int _layoutId;
        readonly int _backgroundResourceId;


        public GenericDialogFragment(int layoutId)
            : this(layoutId, Resource.Color.app_gray_modal_color)
        {

        }

        public GenericDialogFragment(int layoutId, int backgroundResourceId)
        {
            this._layoutId = layoutId;
            this._backgroundResourceId = backgroundResourceId;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            base.EnsureBindingContextSet(savedState);
            var view = this.BindingInflate(_layoutId, null);

            var dialog = new Dialog(Activity);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            dialog.SetContentView(view);
            dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dialog.Window.SetBackgroundDrawableResource(_backgroundResourceId);
            return dialog;
        }

        public override void Show(Android.Support.V4.App.FragmentManager manager, string tag)
        {
            base.Show(manager, tag);
            //We can handle dialog's controls action here using switch based on tag : screen name
        }

    }
    }
}