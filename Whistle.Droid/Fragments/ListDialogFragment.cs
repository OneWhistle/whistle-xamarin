
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using System;
using System.Collections;
using System.Linq.Expressions;
using Whistle.Core.ViewModels;
using Whistle.Droid.Views;

namespace Whistle.Droid.Fragments
{
    public class ListDialogFragment : GenericDialogFragment
    {
        readonly string _header;

        //private Expression<Func<MainViewModel, object>> _bindingItemTo;
        //private Expression<Func<MainViewModel, object>> _bindingItemClickTo;

        private MvxListView InnerListView;

        public IEnumerable ItemSource { get; set; }

        public ListDialogFragment(string header)
            : base(Resource.Layout.dialog_list)
        {
            _header = header;
        }

        //public ListDialogFragment ApplyBindingItemTo(Expression<Func<MainViewModel, object>> propertyTo)
        //{
        //    _bindingItemTo = propertyTo;
        //    return this;
        //}

        //public ListDialogFragment ApplyBindingItemClickTo(Expression<Func<MainViewModel, object>> propertyTo)
        //{
        //    _bindingItemClickTo = propertyTo;
        //    return this;
        //}


        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            InnerListView = view.FindViewById<MvxListView>(Resource.Id.item_list);
            var headerTextView = view.FindViewById<TextView>(Resource.Id.list_header);

            //if (_bindingItemTo != null)
            //{
            //    this.CreateBindingSet<ListDialogFragment, MainViewModel>().Bind(InnerListView).For(m => m.SelectedItem).To(_bindingItemTo).Apply();
            //}
            //if (_bindingItemClickTo != null)
            //    this.CreateBindingSet<ListDialogFragment, MainViewModel>().Bind(InnerListView).For(m => m.ItemClick).To(_bindingItemClickTo).Apply();

            headerTextView.Text = _header;
            InnerListView.ItemsSource = ItemSource;

            return view;
        }

    }
}