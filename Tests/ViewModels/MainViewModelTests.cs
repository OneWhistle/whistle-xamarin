using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Test.Core;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refractored.MvxPlugins.Settings;
using Tests.Mocks;
using Whistle.Core.Services;
using Whistle.Core.ViewModels;

namespace Tests.ViewModels
{

    [TestClass]
    public class MainViewModelTests : MvxIoCSupportingTest
    {
        MainViewModel _tested;
        Mock<IMvxMessenger> _messenger = new Mock<IMvxMessenger>();
        Mock<ISettings> _settings = new Mock<ISettings>();
        Mock<IPhoneService> _phoneService = new Mock<IPhoneService>();

        protected MockMvxViewDispatcher CreateMockNavigation()
        {
            var dispatcher = new MockMvxViewDispatcher();
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            Ioc.RegisterSingleton<IMvxMessenger>(_messenger.Object);
            Ioc.RegisterSingleton<ISettings>(_settings.Object);
            Ioc.RegisterSingleton<IPhoneService>(_phoneService.Object);
            return dispatcher;
        }

        public MainViewModelTests()
        {
            this.Setup();
        }



        [TestMethod]
        public void InitFromBundle_accessToken()
        {
            ClearAll();
            CreateMockNavigation();
            var bundle = new MvxBundle();
            bundle.Data.Add(Whistle.Core.Helpers.Settings.AccessTokenKey, "{accessToken:\"token\"}");
            
            var main = new MainViewModel(_messenger.Object);
            // act
            main.Init(bundle);
            // assert..
            Assert.AreEqual("token", main.NewUser.AccessToken);
        }

        [TestMethod]
        public void InitFromBundle_whistles()
        {
            ClearAll();
            CreateMockNavigation();
            var bundle = new MvxBundle();
            bundle.Data.Add(Whistle.Core.Helpers.Settings.AccessTokenKey, "{accessToken:\"token\", whistles:[{comment:\"test\" }] }");

            var main = new MainViewModel(_messenger.Object);
            // act
            main.Init(bundle);
            // assert..
            Assert.AreEqual("token", main.NewUser.AccessToken);
            Assert.AreEqual(1, main.TrackingViewModel.ConsumerWhistleCollection.Count);
        }

        [TestMethod]
        public void InitFromBundle_complete()
        {
            ClearAll();
            CreateMockNavigation();
            var bundle = new MvxBundle();
            bundle.Data.Add(Whistle.Core.Helpers.Settings.AccessTokenKey, JsonResources.InitFromBundle_complete);

            var main = new MainViewModel(_messenger.Object);
            // act
            main.Init(bundle);
            // assert..
            Assert.AreEqual(JsonResources.InitFromBundle_complete_token, main.NewUser.AccessToken);
            Assert.AreEqual(5, main.TrackingViewModel.ConsumerWhistleCollection.Count);
        }

    }



}
