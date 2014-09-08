using Android.Content;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Refractored.MvxPlugins.Settings;
using Refractored.MvxPlugins.Settings.Droid;

namespace Whistle.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.RegisterSingleton<ISettings>(() => new MvxAndroidSettings());
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }


        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<Button>("Selector", btn => new MvxSelectorBinding(btn));
            base.FillTargetFactories(registry);
        }

        public override void LoadPlugins(Cirrious.CrossCore.Plugins.IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Messenger.PluginLoader>();
            base.LoadPlugins(pluginManager);
        }
    }
}