

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

        public ListItemHelper(MvxActionBarActivity activity)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "ListItemHelper creation");
            this._activity = activity;
            Packages = new List<ListItem>();
            Rides = new List<ListItem>();
        }

        public void OnResume()
        {
            if (Packages.Count > 0)
                return;
            var editVM = (MainViewModel)_activity.ViewModel;
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
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_packages)) { ItemSource = Packages })
    .ApplyBindingItemTo(vm => vm.WhistleEditViewModel.SelectedPackageItem)
    .Show(_activity.SupportFragmentManager, "select_packages");
                    break;
                case "RIDE":
                    (new ListDialogFragment(_activity.GetString(Resource.String.d_choose_rides)) { ItemSource = Rides, ViewModel = _activity.ViewModel })
    .ApplyBindingItemTo(vm => vm.WhistleEditViewModel.SelectedRideItem)
    .Show(_activity.SupportFragmentManager, "select_rides");
                    break;
                default:

                    break;
            }
        }
    }
}
