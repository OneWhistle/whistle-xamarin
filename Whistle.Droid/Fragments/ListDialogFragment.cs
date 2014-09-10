
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using System;
using System.Collections;
using System.Linq.Expressions;
using Whistle.Core.ViewModels;

namespace Whistle.Droid.Fragments
{
    public class ListDialogFragment : GenericDialogFragment
    {
        readonly string _header;

        private Expression<Func<MainViewModel, object>> _bindingTo;

        private MvxListView InnerListView;

        public IEnumerable ItemSource { get; set; }

        public ListDialogFragment(string header)
            : base(Resource.Layout.dialog_list)
        {
            _header = header;
        }

        public ListDialogFragment ApplyBindingTo(Expression<Func<MainViewModel, object>> propertyTo)
        {
            _bindingTo = propertyTo;
            return this;
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            InnerListView = view.FindViewById<MvxListView>(Resource.Id.item_list);
            var headerTextView = view.FindViewById<TextView>(Resource.Id.list_header);

            
            if (_bindingTo != null)
            {
                // apply selected item  binding.
                this.CreateBindingSet<ListDialogFragment, MainViewModel>().Bind(InnerListView).For(p => p.SelectedItem).To(_bindingTo).Apply();
            }

            headerTextView.Text = _header;
            InnerListView.ItemsSource = ItemSource;
            
            return view;
        }

    }
}