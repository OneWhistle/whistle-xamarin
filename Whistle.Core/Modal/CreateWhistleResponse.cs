using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class CreateWhistleResponse
    {
        public Whistle NewWhistle { get; set; }

        public Whistle[] MatchingWhisltes { get; set; }
    }
}
