using System.Collections.Generic;
using Whistle.Core.Modal;

namespace Whistle.Core.ViewModels
{
    public class WhistleResultViewModel
    {
        public IList<Whistle.Core.Modal.MatchingWhistle> WhistleList { get; private set; }


        public WhistleResultViewModel(IList<MatchingWhistle> result)
        {
            this.WhistleList = result;
        }
    }
}
