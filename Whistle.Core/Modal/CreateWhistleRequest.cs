using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class CreateWhistleRequest
    {
        public Whistle Whistle { get; set; }
        public int Limit { get; set; }
        public int Radius { get; set; }
        public bool IsNewWhistle { get; set; }

        public CreateWhistleRequest()
        {
            Limit = 10;
            IsNewWhistle = true;
            Radius = 10;

        }
    }
}
