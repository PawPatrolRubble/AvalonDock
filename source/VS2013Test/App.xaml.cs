using System.Windows;
using AvalonDock;
using Lan.AvalonDock.PrismTest;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VS2013Test.Views;

namespace VS2013Test
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainView>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IDockingNavigationService, DockingNavigationService>();
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var regionManager = Container.Resolve<IRegionManager>();

			regionManager.RequestNavigate("ContentRegion", nameof(Page2), s =>
			{
				;
			});

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
