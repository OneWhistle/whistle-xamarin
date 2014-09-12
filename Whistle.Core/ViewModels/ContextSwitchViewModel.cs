using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.ViewModels
{
    public class ContextSwitchViewModel
    {
        public bool IsConsumerChecked { get; set; }
        public bool IsProviderChecked { get; set; }
        public bool IsTrackingChecked { get; set; }


        public ContextSwitchViewModel()
        {
            //Default:
            IsConsumerChecked = true;
        }
    }
}
