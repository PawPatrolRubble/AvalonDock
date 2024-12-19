using System.Windows;
using AvalonDock;
using Lan.AvalonDock.PrismTest.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Lan.AvalonDock.PrismTest
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App:PrismApplication
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{

		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var regionManager = Container.Resolve<IRegionManager>();

			regionManager.RequestNavigate("ContentRegion", nameof(FileViewA));

		}
		protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
		{
			base.ConfigureRegionAdapterMappings(mappings);
			mappings.RegisterMapping(typeof(DockingManager), new DockingManagerRegionAdapter(Container.Resolve<IRegionBehaviorFactory>()));
		}


		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			base.ConfigureModuleCatalog(moduleCatalog);
			moduleCatalog.AddModule<MainModule>();
		}
	}
}
