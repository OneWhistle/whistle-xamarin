
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using System.Collections;

namespace Whistle.Droid.Fragments
{
    public class ListDialogFragment : GenericDialogFragment
    {
        readonly string _header;

        public IEnumerable ItemSource { get; set; }

        public ListDialogFragment(string header)
            : base(Resource.Layout.dialog_list)
        {
            _header = header;
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var listview = view.FindViewById<MvxListView>(Resource.Id.item_list);
            var headerTextView = view.FindViewById<TextView>(Resource.Id.list_header);

            headerTextView.Text = _header;
            listview.ItemsSource = ItemSource;

            return view;
        }

    }
}