
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Whistle.Droid.Adepters;

namespace Whistle.Droid.Fragments
{
    public class ContentMenuFragment : MvxFragment
    {
        Android.Support.V4.App.Fragment newContent = null;
        ListView listView;

        public ContentMenuFragment()
        {
                
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.List, null);
            listView = view.FindViewById<ListView>(Resource.Id.listMenu);
            listView.ItemClick += OnListItemClick;
            //OnListItemClick;
            return view;
        }
  
        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);

            var colorAdapter = new MenuAdapter(Activity.ApplicationContext); //new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, new List<string>() { "R", "RR", "RRR", "RRRR" });
            newContent = new WhistleFragment(); // hum hum....
            SwitchFragment(newContent);
           
            listView.Adapter = colorAdapter;
            
        }

        public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs args)
        {
            switch (args.Position)
            {
                case 0:
                    newContent = new WhistleFragment();
                    break;
                case 1:
                    newContent = new EditFragment();
                    break;
                case 2:
                    newContent = new FavoriteFragment();
                    break;
                case 3:
                    newContent = new SettingFragments();
                    break;
                case 4:
                    newContent = new AboutFragments();
                    break;
                case 5:
                    newContent = new WhistleWorkFragment();
                    break;
                case 6:
                    Intent intent = new Intent(View.Context, typeof(Whistle.Droid.Views.LandingView));
                    StartActivity(intent);
                    break;
            }

            if (newContent != null)
                SwitchFragment(newContent);
        }
        private void SwitchFragment(Android.Support.V4.App.Fragment fragment)
        {
            if (Activity == null)
                return;

            var fca = Activity as WhistleSlidingFragmentActivity;
            if (fca != null)
                fca.SwitchContent(fragment);

        }
    }
}