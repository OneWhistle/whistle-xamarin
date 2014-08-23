using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ListFragment = Android.Support.V4.App.ListFragment;
using Fragment = Android.Support.V4.App.Fragment;
using Whistle.Droid.Adepters;

namespace Whistle.Droid.Fragments
{
    public class ContentMenuFragment : ListFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup p1, Bundle p2)
        {
            return inflater.Inflate(Resource.Layout.List, null);
        }

        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);

            var colorAdapter = new MenuAdapter(Activity.ApplicationContext); //new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, new List<string>() { "R", "RR", "RRR", "RRRR" });
            ListAdapter = colorAdapter;
        }
        public override void OnListItemClick(ListView p0, View p1, int position, long p3)
        {
            Fragment newContent = null;
            switch (position)
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
                    Intent intent = new Intent(View.Context, typeof(LandingActivity));
                    StartActivity(intent);
                    break;
            }

            if (newContent != null)
                SwitchFragment(newContent);
        }
        private void SwitchFragment(Fragment fragment)
        {
            if (Activity == null)
                return;

            var fca = Activity as MenuFragmentActivity;
            if (fca != null)
                fca.SwitchContent(fragment);

            var ra = Activity as MenuFragmentActivity;
            if (ra != null)
            {
                ra.SwitchContent(fragment);
            }
        }
    }
}