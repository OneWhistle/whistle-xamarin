

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Whistle.Core.ViewModels;

namespace Whistle.Droid.Fragments
{
    public class GenericFragment : MvxFragment
    {
        readonly int _layoutId;
        readonly int _menuIconRes;

        public GenericFragment(int layoutId, int menuIconRes)
        {
            _layoutId = layoutId;
            _menuIconRes = menuIconRes;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            GenericDialogFragment busyFrag = new GenericDialogFragment(Resource.Layout.BusyIndicator) { ViewModel = this.ViewModel };
            
           // busyFrag.
            //Adding Busy view
            ((BaseViewModel)ViewModel).IsBusyChanged = (busy) =>
            {
                if (busy)
                    busyFrag.Show(FragmentManager, "BusyIndicator");
                else
                    busyFrag.Dialog.Hide();
            };               

            return this.BindingInflate(_layoutId, null);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            var item = menu.Add(Resource.String.menu_help);
            item.SetIcon(_menuIconRes);
            item.SetShowAsAction(ShowAsAction.Always);
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
     

        public GenericDialogFragment(int layoutId): this(layoutId, Resource.Color.app_gray_modal_color)
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
    }
}