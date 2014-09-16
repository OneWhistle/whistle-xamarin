
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Whistle.Droid.Fragments
{
    public class TrackingFragment: GenericFragment
    {
        public TrackingFragment(): base(Resource.Layout.tracking, Resource.Menu.menu_switch)
        {

        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var tabHost = view as Android.Widget.TabHost;

            if (tabHost != null)
            {
                tabHost.Setup();
                var indicatorConsumer = inflater.Inflate(Resource.Layout.tab_consumer, null);
                var indicatorProvider= inflater.Inflate(Resource.Layout.tab_consumer, null);
                var cons = tabHost.NewTabSpec("consumer").SetIndicator("CONSUMER").SetContent(Resource.Id.consumer);
                var prov = tabHost.NewTabSpec("provider").SetIndicator("PROVIDER").SetContent(Resource.Id.provider);


                tabHost.AddTab(cons);
                tabHost.AddTab(prov);

                tabHost.CurrentTab = 0;
            }

            
            return view;
        }

    }
}