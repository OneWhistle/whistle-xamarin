
using Android.App;
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
        public override Dialog OnCreateDialog(Bundle savedState)
        {
            var dialog = base.OnCreateDialog(savedState);
            var listview = dialog.FindViewById<MvxListView>(Resource.Id.item_list);
            var headerTextView = dialog.FindViewById<TextView>(Resource.Id.list_header);

            headerTextView.Text = _header;
           // listview.AddHeaderView(textView);
            listview.ItemsSource = ItemSource;

            return dialog;
        }
    }
}