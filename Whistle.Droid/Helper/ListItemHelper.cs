

using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using System.Collections.Generic;
using Whistle.Core.ViewModels;
using Whistle.Droid.Fragments;
namespace Whistle.Droid.Helper
{


    public class ListItemHelper
    {
        MvxActionBarActivity _activity;

        public List<ListItem> Packages { get; private set; }
        public List<ListItem> Rides { get; private set; }
        public List<ListItem> Countries { get; private set; }
        public List<ListItem> Cities { get; private set; }

        public ListItemHelper(MvxActionBarActivity activity)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "ListItemHelper creation");
            this._activity = activity;
            Packages = new List<ListItem>();
            Rides = new List<ListItem>();
            //for demo
            Countries = new List<ListItem>();
            Cities = new List<ListItem>();
        }

        public void OnResume()
        {
            if (Packages.Count > 0)
                return;
            var editVM = (BaseViewModel)_activity.ViewModel;
            var listRide = _activity.Resources.GetStringArray(Resource.Array.lst_ride);
            var listPackage = _activity.Resources.GetStringArray(Resource.Array.lst_package);
            //country & city
            var listcontries = _activity.Resources.GetStringArray(Resource.Array.lst_country);
            var listcity = _activity.Resources.GetStringArray(Resource.Array.lst_city);
            int position = 0;

            foreach (var ride in listRide)
            {
                var itm = new ListItem { DisplayName = ride, Position = position };
                itm.ParentSelectionChanged = editVM.WhistleEditViewModel.OnSelectionChanged; //ride selection
                Rides.Add(itm);
                position++;
            }
            position = 0;
            foreach (var p in listPackage)
            {
                var itm = new ListItem { DisplayName = p, Position = position };
                itm.ParentSelectionChanged = editVM.WhistleEditViewModel.OnPackageSelectionchanged;
                Packages.Add(itm);
                position++;
            }

            position = 0;
            foreach (var count in listcontries)
            {
                var itm = new ListItem { DisplayName = count, Position = position };
                itm.ParentSelectionChanged = editVM.WhistleEditViewModel.OnContrySelectionChanged;
                Countries.Add(itm);
                position++;
            }
            position = 0;
            foreach (var city in listcity)
            {
                var itm = new ListItem { DisplayName = city, Position = position };
                itm.ParentSelectionChanged = editVM.WhistleEditViewModel.OnSelectionChanged;
                Cities.Add(itm);
                position++;
            }
        }

        public void ShowList(string listType)
        {
            switch (listType)
            {
                case "PACKAGES":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_packages)) { ItemSource = Packages })
                        //.ApplyBindingItemTo(vm => vm.WhistleEditViewModel.SelectedPackageItem)
                        .Show(_activity.SupportFragmentManager, "select_packages");
                    break;
                case "RIDE":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_rides)) { ItemSource = Rides })
                        //.ApplyBindingItemTo(vm => vm.WhistleEditViewModel.SelectedRideItem)   
                        .Show(_activity.SupportFragmentManager, "select_rides");
                    break;
                case "COUNTRY":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_country)) { ItemSource = Countries })
                        .Show(_activity.SupportFragmentManager, "select_countries");
                    break;
                case "CITY":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_city)) { ItemSource = Cities })
                        .Show(_activity.SupportFragmentManager, "select_city");
                    break;
                default:

                    break;
            }
        }
    }
}
