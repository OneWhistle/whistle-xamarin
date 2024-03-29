using System.Collections.Specialized;
using System.Windows.Input;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore.IoC;

namespace Whistle.Droid
{
    // This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
    // are preserved in the deployed app
    public class LinkerPleaseInclude
    {
        public void Include(MvxPropertyInjector injector)
        {
            injector = new MvxPropertyInjector();
        }
        public void Include(Whistle.Core.ViewModels.LandingViewModel injector)
        {
            //Need to see
            //injector = new Whistle.Core.ViewModels.LandingViewModel();
        }
       

        public void Include(Button button)
        {
            button.Click += (s,e) => button.Text = button.Text + "";
        }
        public void Include(ImageButton button)
        {
            button.Click += (s, e) => { };
        }

        public void Include(CheckBox checkBox)
        {
            checkBox.CheckedChange += (sender, args) => checkBox.Checked = !checkBox.Checked;
        }

        public void Include(Android.Gms.Maps.MapView mapview)
        {
            mapview.Clickable = mapview.Clickable;
        }

        public void Include(View view)
        {
            view.Click += (s, e) => view.ContentDescription = view.ContentDescription + "";
        }

        public void Include(TextView text)
        {
            text.Text = text.Text + "";
            text.TextChanged += (sender, args) => text.Text = "" + text.Text;
			text.Hint = "" + text.Hint;
        }

        public void Include(Whistle.Droid.Views.WhistleTextView text)
        {
            text.Text = text.Text + "";
            text.TextChanged += (sender, args) => text.Text = "" + text.Text;
            text.Hint = "" + text.Hint;
        }


        public void Include(CompoundButton cb)
        {
            cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked;
        }

        public void Include(SeekBar sb)
        {
            sb.ProgressChanged += (sender, args) => sb.Progress = sb.Progress + 1;
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s,e) => { var test = string.Format("{0}{1}{2}{3}{4}", e.Action,e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex); } ;
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
        }
    }
}
