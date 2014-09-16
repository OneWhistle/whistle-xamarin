
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Whistle.Core.Modal;
using System.Linq;
using System.Collections.ObjectModel;
using Cirrious.CrossCore;
using Whistle.Core.Interfaces;
using Cirrious.CrossCore.Platform;
namespace Whistle.Core.ViewModels
{

    public class ListItem : BaseEntity
    {
        public int Position { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
                if (ParentSelectionChanged != null)
                    ParentSelectionChanged(this);
            }
        }

        internal void UnSelect()
        {
            this.isSelected = false;
            OnPropertyChanged("IsSelected");
        }

        public Action<ListItem> ParentSelectionChanged;

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public class WhistleEditViewModel : BaseEntity
    {
        public CustomLocation SourcePoint { get; set; }
        public CustomLocation DestinationPoint { get; set; }


        public string JourneyMessage { get; set; }

        private string _sourceLocation;
        public string SourceLocation
        {
            get { return _sourceLocation; }
            set { _sourceLocation = value; OnPropertyChanged("SourceLocation"); }
        }


        private ListItem selectedRideItem;
        public ListItem SelectedRideItem
        {
            get { return selectedRideItem; }
            set
            {
                selectedRideItem = value;
                OnPropertyChanged("SelectedRideItem");
            }
        }

        readonly List<int> _selectedPackageList = new List<int>();
        public IList<int> SelectedPackageList { get { return _selectedPackageList; } }

        readonly ObservableCollection<string> _destinationSuggestion = new ObservableCollection<string>();
        public ObservableCollection<string> DestinationSuggestion { get { return _destinationSuggestion; } }

        private string _destinationHint;
        public string DestinationHint
        {
            get { return _destinationHint; }
            set
            {
                _destinationHint = value;
                if (_destinationHint == null
                    || _destinationHint.Trim().Length < 5)
                {
                    DestinationSuggestion.Clear();
                    return;
                }
                locationLookup(_destinationHint, DestinationSuggestion);
            }
        }


        private string _destinationLocation;
        public string DestinationLocation
        {
            get { return _destinationLocation; }
            set { _destinationLocation = value; OnPropertyChanged("DestinationLocation"); }
        }

        //private MvxCommand setDestinationSelection;
        //public ICommand SetDestinationSelection { get { return this.setDestinationSelection ?? (this.setDestinationSelection = new MvxCommand(onSetDestinationSelection)); } }

        //private MvxCommand setSourceSelection;
        //public ICommand SetSourceSelection { get { return this.setSourceSelection ?? (this.setSourceSelection = new MvxCommand(onSetSourceSelection)); } }

        public void OnRideSelectionChanged(ListItem item)
        {
            if (this.SelectedRideItem != null)
                this.SelectedRideItem.UnSelect();
            this.SelectedRideItem = item;
        }

        public void OnPackageSelectionchanged(ListItem item)
        {
            if (item.IsSelected == false)
                _selectedPackageList.Remove(item.Position);
            else
                _selectedPackageList.Add(item.Position);
            this.OnPropertyChanged("SelectedPackageList");
        }


        public WhistleEditViewModel()
        {
            //SourceLocationMode = false;
        }

        //private void onSetDestinationSelection()
        //{
        //    SourceLocationMode = false;
        //    DestinationLocationMode = true;
        //    OnPropertyChanged("SourceLocationMode");
        //    OnPropertyChanged("DestinationLocationMode");
        //}

        //private void onSetSourceSelection()
        //{
        //    SourceLocationMode = true;
        //    DestinationLocationMode = false;
        //    OnPropertyChanged("SourceLocationMode");
        //    OnPropertyChanged("DestinationLocationMode");
        //}

        public Whistle.Core.Modal.Whistle GetNewWhistle()
        {
            var whistle = new Modal.Whistle
            {
                Type = RideTypeConstants.All[selectedRideItem.Position],
                Public = true,
                //LeavingLocation = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.GeographicPosition(SourcePoint.Item1, SourcePoint.Item2)),
                DestinationLocation = DestinationPoint,
                Size = _selectedPackageList.ToArray(),
                Comment = JourneyMessage
            };
            return whistle;
        }



        public string[] IsValid()
        {
            if (SourcePoint == null)
                return new[] { "missing current location" };
            if (DestinationPoint == null)
                return new[] { "missing destination location" };
            if (_selectedPackageList.Count == 0 || SelectedRideItem == null)
                return new[] { "missing either package or ride type" };
            //if (string.IsNullOrEmpty(JourneyMessage))
            //    return false;
            return new string[] { };
        }

        public void UpdatePosition(double longitude, double latitude, bool source = true)
        {
            if (source)
                SourcePoint = new CustomLocation(longitude, latitude);
            else
                DestinationPoint = new CustomLocation(longitude, latitude);
        }

        private async void locationLookup(string hint, ObservableCollection<string> targetCollection)
        {
            Mvx.Trace(MvxTraceLevel.Diagnostic, "locationLookup {0}", hint);
            var service = Mvx.Resolve<IAddressService>();
            var result =  await service.Loopkup(hint);

            targetCollection.Clear();
            foreach (var item in result)
                targetCollection.Add(item);
        }

    }
}
