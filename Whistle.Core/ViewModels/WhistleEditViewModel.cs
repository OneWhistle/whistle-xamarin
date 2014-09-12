
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Whistle.Core.Modal;
using System.Linq;
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
        string[] rideTypes = new[]
            {
                "BIKE",
                "AUTO",
                "SMALL_CAR",
                "LARGE_CAR",
                "MINI_BUS",
                "BUS",
                "TRUCK",
                "TRAIN",
                "FLIGHT"
            };
        //public bool SourceLocationMode { get; set; }
        //public bool DestinationLocationMode { get; set; }

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

        readonly IList<ListItem> _selectedPackageList = new List<ListItem>();

        public string SelectedPackageText
        {
            get
            {
                if (_selectedPackageList.Count == 0) return null;
                return _selectedPackageList[0].DisplayName;
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
                _selectedPackageList.Remove(item);
            else
                _selectedPackageList.Add(item);
            this.OnPropertyChanged("SelectedPackageText");
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
                Type = rideTypes[selectedRideItem.Position],
                Public = true,
                //LeavingLocation = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.GeographicPosition(SourcePoint.Item1, SourcePoint.Item2)),
                DestinationLocation = DestinationPoint,
                Size = _selectedPackageList.Select(c => c.Position).ToArray(),
                Comment = JourneyMessage
            };
            return whistle;
        }



        public bool IsValid()
        {
            if (SourcePoint == null || DestinationPoint == null)
                return false;
            if (_selectedPackageList.Count == 0 && SelectedRideItem == null)
                return false;
            if (string.IsNullOrEmpty(JourneyMessage))
                return false;
            return true;
        }

        public void UpdatePosition(double p1, double p2, bool source = true)
        {
            if (source)
                SourcePoint = new CustomLocation(p1, p2);
            else
                DestinationPoint = new CustomLocation(p1, p2);
        }
    }
}
