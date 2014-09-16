using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whistle.Core.ViewModels;
using Newtonsoft.Json;

namespace Tests.ViewModels
{
    [TestClass]
    public class WhistleEditViewModelTests
    {
        [TestMethod]
        public void GetNewWhistle()
        {
            var vm = new WhistleEditViewModel();
            vm.SelectedRideItem = new ListItem{ Position = 0};

            var result = vm.GetNewWhistle();
            //asserts.
            Assert.IsFalse(result.Provider);
        }
    }
}
