using Cirrious.CrossCore.IoC;
using Whistle.Core.ViewModels;

namespace Whistle.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            //RegisterAppStart<LandingViewModel>();
        }
    }
}