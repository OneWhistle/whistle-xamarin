

using Android.App;
using Android.OS;
using Android.Views;
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
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(_layoutId, null);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(_menuResId, menu);  
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public void RegisterControls()
        {

        }
    }   
}