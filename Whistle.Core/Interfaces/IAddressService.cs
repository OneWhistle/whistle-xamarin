using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Interfaces
{
    public interface IAddressService
    {
        Task<IList<string>> Loopkup(string hint);
    }
}
