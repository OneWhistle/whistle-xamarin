

using System.Collections.Generic;
using Whistle.Core.ViewModels;
using Whistle.Droid.Fragments;
namespace Whistle.Droid.Helper
{
   

    public class ListItemHelper
    {
        //string[] packageList = new[] { "ENVELOPS", "SMALL (UP TO 10 KG)", "MEDIUM (BETWEEN 11 - 50 KG)", "LARGE (BETWEEN 51 - 100 KG)", "EXTRA LARGE (MORE THAN 100 KG)" };
        //string[] rideList = new[] { "BIKE(2 SEATS)", "AUTO(3 SEATS)", "SMALL CAR(4 SEATS)", "LARGE CAR(6 SEATS)", "MINI BUS(12 SEATS)", "BUS(20+ SEATS)", "TRUCK(ONLY PACKAGE)", "TRAIN", "FLIGHT" };

        MvxActionBarActivity _activity;

        public List<ListItem> Packages { get; private set; }
        public List<ListItem> Rides { get; private set; }

        public ListItemHelper(MvxActionBarActivity activity)
        {
            this._activity = activity;
            Packages = new List<ListItem>();
            Rides = new List<ListItem>();
        }

        public void OnResume()
        {
            if (Packages.Count > 0)
                return;
            var editVM = (MainViewModel) _activity.ViewModel;
            var listRide = _activity.Resources.GetStringArray(Resource.Array.lst_ride);
            var listPackage = _activity.Resources.GetStringArray(Resource.Array.lst_package);

            foreach (var r in listRide)
            {
                var itm = new ListItem { DisplayName = r };
                itm.ParentSelectionChanged = editVM.WhistleEditViewModel.OnRideSelectionChanged;
                Rides.Add(itm);
            }

            foreach (var p in listPackage)
            {
                var itm = new ListItem { DisplayName = p };
                itm.ParentSelectionChanged = editVM.WhistleEditViewModel.OnPackageSelectionchanged;
                Packages.Add(itm);
            }            
        }

        public void ShowList(string listType)
        {
            switch (listType)
            {
                case "PACKAGES":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_packages)) { ItemSource = Packages})
    .ApplyBindingItemTo(vm => vm.WhistleEditViewModel.SelectedPackageItem)
    .Show(_activity.SupportFragmentManager, "select_items");
                    break;
                case "RIDE":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_rides)) { ItemSource = Rides, ViewModel = _activity.ViewModel })
    .ApplyBindingItemTo(vm => vm.WhistleEditViewModel.SelectedRideItem)
    .Show(_activity.SupportFragmentManager, "select_items");
                    break;
                default :
                   
                    break;
            }
        }
    }
}
