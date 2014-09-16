using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistle.Core.Interfaces;

namespace Whistle.Core.Services
{
    public class AddressService : IAddressService
    {

        public async Task<IList<string>> Loopkup(string hint)
        {
            return new string[] { };
        }
    }
}
