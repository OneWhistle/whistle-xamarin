
using Android.Gms.Maps;
using Android.OS;
using Android.Views;

namespace Whistle.Droid.Fragments
{
    public class MapHostFragment : GenericFragment
    {
        public MapHostFragment(int layoutId, int menuIcon):base(layoutId, menuIcon)
        {
           
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
         
            var map = new SupportMapFragment();
                        
            this.Activity.SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.map, map)
                .Commit();

            return view; 
        }
    }
}