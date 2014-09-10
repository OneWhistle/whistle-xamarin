
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using Whistle.Core.Modal;
namespace Whistle.Core.ViewModels
{
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

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(JourneyMessage))
                return false;
            return true;
        }
    }
}
