using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.ViewModels
{
    public class WhistleItemViewModel
    {
        public Modal.Whistle Whistle {get; private set;}

        public WhistleItemViewModel(Modal.Whistle item)
        {
            this.Whistle = item;
        }
    }
}
