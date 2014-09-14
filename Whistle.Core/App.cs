using Cirrious.CrossCore.IoC;
using Segment;
using Whistle.Core.ViewModels;

namespace Whistle.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            Analytics.Initialize("nrxalgi1m1");
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<LandingViewModel>();
        }
    }
}