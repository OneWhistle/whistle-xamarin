using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class MatchingWhistle
    {
        [JsonProperty(PropertyName = "dis")]
        public int Dis { get; set; }
        [JsonProperty(PropertyName = "obj")]
        public User Obj { get; set; }
    }
}
