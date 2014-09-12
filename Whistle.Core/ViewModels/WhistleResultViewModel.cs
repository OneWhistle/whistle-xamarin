using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.ViewModels
{
    public class WhistleResultViewModel
    {
        public IList<Whistle.Core.Modal.Whistle> WhistleList { get; private set; }


        public WhistleResultViewModel(IList<Whistle.Core.Modal.Whistle> result)
        {
            this.WhistleList = result;
        }
    }
}
