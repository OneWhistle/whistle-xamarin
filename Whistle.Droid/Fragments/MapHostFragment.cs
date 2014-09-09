
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace Whistle.Droid.Fragments
{
    public class MapHostFragment : GenericFragment
    {
        SupportMapFragment mapFragment;

        public MapHostFragment(SupportMapFragment mapFragment, int layoutId, int menuRes)
            : base(layoutId, menuRes)
        {
            this.mapFragment = mapFragment;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            // Getting GoogleMap object from the fragment
         //   Task.Factory.StartNew(waitForMapToResume);
        }

        //private void waitForMapToResume()
        //{
        //    while (!mapFragment.IsResumed)
        //    {
        //        Task.Delay(100);
        //    }

        //    if (mapFragment.Map != null)
        //    {

        //        // Getting LocationManager object from System Service LOCATION_SERVICE
        //        LocationManager locationManager = (LocationManager)this.Activity.GetSystemService(Context.LocationService);

        //        // Creating a criteria object to retrieve provider
        //        Criteria criteria = new Criteria();
        //        // Getting the name of the best provider
        //        var provider = locationManager.GetBestProvider(criteria, true);
        //        // Getting Current Location
        //        Location location = locationManager.GetLastKnownLocation(provider);

        //        if (location != null)
        //        {
        //            updateLocation(mapFragment.Map, location);
        //        }
        //    }

        //}


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            this.ChildFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.map, mapFragment)
                .Commit();

            return view;
        }

        public override void OnDetach()
        {
            //this.ChildFragmentManager
            //    .BeginTransaction()
            //    .Remove(mapFragment)
            //    .Commit();

            base.OnDetach();
        }


        //    protected void updateLocation(GoogleMap map, Location location)
        //    {
        //        // Creating a LatLng object for the current location
        //        LatLng latLng = new LatLng(location.Latitude, location.Longitude);
        //        // Showing the current location in Google Map
        //        map.MoveCamera(CameraUpdateFactory.NewLatLng(latLng));
        //        // Zoom in the Google Map
        //        //map.AnimateCamera(CameraUpdateFactory.ZoomTo(15));
        //    }
        //}
    }
}