
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Whistle.Core.Modal;
namespace Whistle.Core.ViewModels
{

    public class ListItem : BaseEntity
    {
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
        public bool SourceLocationMode { get; set; }
        public bool DestinationLocationMode { get; set; }


        public string JourneyMessage { get; set; }

        private string _sourceLocation;
        public string SourceLocation
        {
            get { return _sourceLocation; }
            set { _sourceLocation = value; OnPropertyChanged("SourceLocation"); }
        }

        private object selectedPackageItem;
        public object SelectedPackageItem
        {
            get { return selectedPackageItem; }
            set { selectedPackageItem = value; OnPropertyChanged("SelectedPackageItem"); }
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

        private IList<ListItem> _selectedPackageList = new List<ListItem>();


        private string _destinationLocation;
        public string DestinationLocation
        {
            get { return _destinationLocation; }
            set { _destinationLocation = value; OnPropertyChanged("DestinationLocation"); }
        }

        private MvxCommand setDestinationSelection;
        public ICommand SetDestinationSelection { get { return this.setDestinationSelection ?? (this.setDestinationSelection = new MvxCommand(onSetDestinationSelection)); } }

        private MvxCommand setSourceSelection;
        public ICommand SetSourceSelection { get { return this.setSourceSelection ?? (this.setSourceSelection = new MvxCommand(onSetSourceSelection)); } }

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
        }


        public WhistleEditViewModel()
        {
            SourceLocationMode = true;
        }

        private void onSetDestinationSelection()
        {
            SourceLocationMode = false;
            DestinationLocationMode = true;
            OnPropertyChanged("SourceLocationMode");
            OnPropertyChanged("DestinationLocationMode");
        }

        private void onSetSourceSelection()
        {
            SourceLocationMode = true;
            DestinationLocationMode = false;
            OnPropertyChanged("SourceLocationMode");
            OnPropertyChanged("DestinationLocationMode");
        }

        public Whistle.Core.Modal.Whistle GetNewWhistle()
        {
            return new Modal.Whistle
            {
                Type = "taxi",
                Public = true,
                Provider = false,
                Size = new[] { 4 },
                Comment = JourneyMessage
            };
        }



        public bool IsValid()
        {

            if (string.IsNullOrEmpty(JourneyMessage))
                return false;
            return true;
        }
    }
}
