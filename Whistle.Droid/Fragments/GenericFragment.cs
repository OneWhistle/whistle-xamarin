

using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Whistle.Droid.Fragments
{
    public class GenericFragment: MvxFragment
    {
        readonly int _layoutId;
        public GenericFragment(int layoutId)
        {
            _layoutId = layoutId;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(_layoutId, null);
        }
    }
}