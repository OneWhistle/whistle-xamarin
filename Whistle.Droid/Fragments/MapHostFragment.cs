
using Android.Gms.Maps;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;

namespace Whistle.Droid.Fragments
{
    public class MapHostFragment : GenericFragment
    {
        MapView _mapView;
        LinearLayout _mapHost;

        public MapHostFragment(MapView mapView, int layoutId, int menuRes)
            : base(layoutId, menuRes)
        {
            this._mapView = mapView;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _mapHost = view.FindViewById<LinearLayout>(Resource.Id.map);
            _mapView.Map.UiSettings.MyLocationButtonEnabled = true;
            _mapView.Map.UiSettings.ZoomControlsEnabled = true;
            _mapView.Map.UiSettings.CompassEnabled = true;


            _mapHost.AddView(_mapView);
            return view;
        }

        public override void OnDetach()
        {
            _mapHost.RemoveAllViews();
            base.OnDetach();
        }
    }
}