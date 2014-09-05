
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
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

        private void selectOption(int position)
        {
            var mainAct =  (this.Activity as Whistle.Droid.Views.MainView);
           // var viewModel =.ViewModel; // we can do better...

            MvxFragment newContent = null;
            switch (position)
            {
                case 0:
                    newContent = new GenericFragment(Resource.Layout.Whistle, Resource.Drawable.userstatus_available_white_icon);
                    break;
                case 1:
                    newContent = new GenericFragment(Resource.Layout.EditProfile, Resource.Drawable.userstatus_available_white_icon);
                    break;
                case 2:
                    newContent = new GenericFragment(Resource.Layout.Favorite, Resource.Drawable.userstatus_available_white_icon);
                    break;
                case 3:
                    newContent = new GenericFragment(Resource.Layout.Setting, Resource.Drawable.userstatus_available_white_icon);
                    break;
                case 4:
                    newContent = new GenericFragment(Resource.Layout.About, Resource.Drawable.userstatus_available_white_icon);
                    break;
                case 5:
                    newContent = new GenericFragment(Resource.Layout.WhistleWork, Resource.Drawable.userstatus_available_white_icon);
                    break;
                case 6:
                    Intent intent = new Intent(View.Context, typeof(Whistle.Droid.Views.LandingView));
                    StartActivity(intent);
                    return;
            }
            newContent.ViewModel = mainAct.ViewModel; //this causes crash
            if (mainAct != null)
                mainAct.SwitchContent(newContent);
        }

        public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs args)
        {
            selectOption(args.Position);
        }
   
    }
}