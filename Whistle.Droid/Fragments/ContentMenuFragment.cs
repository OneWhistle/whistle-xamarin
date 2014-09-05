
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Whistle.Core.Helpers;
using Whistle.Core.ViewModels;
using Whistle.Droid.Adepters;

namespace Whistle.Droid.Fragments
{
    public class ContentMenuFragment : MvxFragment
    {
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
            return view;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }
        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);

            var menuAdapter = new MenuAdapter(Activity.ApplicationContext);

            listView.Adapter = menuAdapter;
            selectOption(0);
        }


        private string getUserTypeTitle(int userType)
        {
            // this code will go in the core project...
            switch(userType)
            {
                case 0:
                    return "CONSUMER";
                case 1:
                    return "PROVIDER";
                case 2:
                    return "TRACKING";
            };
            return string.Empty;
        }

        private void selectOption(int position)
        {
            var mainAct =  (this.Activity as Whistle.Droid.Views.MainView);
           // var viewModel =.ViewModel; // we can do better...
            string title = string.Empty;
            MvxFragment newContent = null;
            switch (position)
            {
                case 0:
                    newContent = new MapHostFragment(Resource.Layout.Whistle, Resource.Menu.menu_switch);
                    title = getUserTypeTitle(Settings.UserType);
                    break;
                case 1:
                    newContent = new GenericFragment(Resource.Layout.EditProfile, Resource.Menu.menu_switch);
                    title = "EDIT PROFILE";
                    break;
                case 2:
                    newContent = new GenericFragment(Resource.Layout.Favorite, Resource.Menu.menu_switch);
                    title = "MY FAVORITES";
                    break;
                case 3:
                    newContent = new GenericFragment(Resource.Layout.Setting, Resource.Menu.menu_switch);
                    title = "SETTINGS";
                    break;
                case 4:
                    newContent = new GenericFragment(Resource.Layout.About, Resource.Menu.menu_switch);
                    title = "ABOUT WHISTLE";
                    break;
                case 5:
                    newContent = new GenericFragment(Resource.Layout.WhistleWork, Resource.Menu.menu_switch);
                    title = "HOW WHISTLE WORKS";
                    break;
                case 6:
                    Intent intent = new Intent(View.Context, typeof(Whistle.Droid.Views.LandingView));
                    StartActivity(intent);
                    break;
            }
            var viewmodel = (BaseViewModel)mainAct.ViewModel;
            viewmodel.Title = title;
            newContent.ViewModel = viewmodel;
            if (mainAct != null)
                mainAct.SwitchContent(newContent);
        }

     
        public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs args)
        {
            selectOption(args.Position);
        }
   
    }
}