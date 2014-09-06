

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Whistle.Droid.Fragments
{
    public class GenericFragment : MvxFragment
    {
        readonly int _layoutId;
        readonly int _menuResId;

        public GenericFragment(int layoutId, int menuIconRes)
        {
            _layoutId = layoutId;
            _menuResId = menuIconRes;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
<<<<<<< HEAD
=======

>>>>>>> 1812d5a83105490e233af7d709c4082a06c5a85a
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(_layoutId, null);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
<<<<<<< HEAD
            inflater.Inflate(_menuResId, menu);  
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public void RegisterControls()
        {

=======
            inflater.Inflate(_menuResId, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }
    }




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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = this.BindingInflate(_layoutId, container);
            return view;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            base.EnsureBindingContextSet(savedState);
            var dialog = new Dialog(Activity, Android.Resource.Style.ThemeHoloNoActionBarFullscreen);
            //dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            //dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dialog.Window.SetBackgroundDrawableResource(_backgroundResourceId);
            return dialog;
>>>>>>> 1812d5a83105490e233af7d709c4082a06c5a85a
        }
    }   
}