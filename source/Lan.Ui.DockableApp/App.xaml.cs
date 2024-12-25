using Prism.Ioc;

using System.Windows;
using AvalonDock;
using Lan.Ui.Dockable;
using Lan.Ui.Dockable.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Lan.Ui.DockableApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{

		}

		protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
		{
			base.ConfigureRegionAdapterMappings(mappings);
			mappings.RegisterMapping(typeof(DockingManager), new DockingManagerRegionAdapter(Container.Resolve<IRegionBehaviorFactory>()));
		}


		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			base.ConfigureModuleCatalog(moduleCatalog);
			moduleCatalog.AddModule<DockableModule>();
		}
	}
}
