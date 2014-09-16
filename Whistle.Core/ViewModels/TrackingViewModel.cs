using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Whistle.Core.ViewModels
{
    public class TrackingViewModel
    {
        public int Count { get { return ConsumerWhistleCollection.Count + ProviderWhistleCollection.Count; } }

        public ObservableCollection<WhistleItemViewModel> ConsumerWhistleCollection { get; private set; }

        public ObservableCollection<WhistleItemViewModel> ProviderWhistleCollection { get; private set; }

        public TrackingViewModel(IList<Whistle.Core.Modal.Whistle> whistleList)
        {
            ConsumerWhistleCollection = new ObservableCollection<WhistleItemViewModel>();
            ProviderWhistleCollection = new ObservableCollection<WhistleItemViewModel>();

                        if (whistleList == null)
                return;

            foreach (var whistle in whistleList)
            {
                AddNewWhisle(whistle);
            }
        }

        public void AddNewWhisle(Modal.Whistle item)
        {
              if (!item.Provider)
                ConsumerWhistleCollection.Add(new WhistleItemViewModel(item));
              else
                  ProviderWhistleCollection.Add(new WhistleItemViewModel(item));

        }

    }
}
