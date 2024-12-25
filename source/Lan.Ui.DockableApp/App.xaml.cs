using Prism.Ioc;

using System.Windows;
using AvalonDock;
using Lan.Ui.Dockable;
using Lan.Ui.Dockable.Views;
using Prism.DryIoc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Ioc;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Lan.Ui.DockableApp.ViewModels;
using Lan.Ui.DockableApp.Views;
using Prism.Commands;

namespace Lan.Ui.DockableApp
{

	public static class GlobalCommand
	{

		public static ICommand GoToPageCommand
		{
			get;
			set;
		}


	}

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IDockableMainViewContentProvider, DockableMainViewContentProvider>();
			containerRegistry.RegisterForNavigation<DocumentA, DocumentAViewModel>();
			containerRegistry.RegisterForNavigation<B1, B1ViewModel>();
		}

		protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
		{
			base.ConfigureRegionAdapterMappings(mappings);
			mappings.RegisterMapping(typeof(DockingManager), new DockingManagerRegionAdapter(Container.Resolve<IRegionBehaviorFactory>(), Container.Resolve<IDockableMainViewContentProvider>()));
		}


		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			base.ConfigureModuleCatalog(moduleCatalog);
			moduleCatalog.AddModule<DockableModule>();
		}


		private void GoToPage(object viewName)
		{
			if (viewName is string viewNameString)
			{

				var regionManager = Container.Resolve<IRegionManager>();

				regionManager.RequestNavigate("ContentRegion", viewNameString);
			}
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			GlobalCommand.GoToPageCommand = new DelegateCommand<object>(GoToPage);

			var provider = Container.Resolve<IDockableMainViewContentProvider>();
			provider.Menus.AddRange(new MenuItem[]{

					new MenuItem{
						Header="go to tests",
						Command =GlobalCommand.GoToPageCommand,
						CommandParameter = nameof(DocumentA)
					}	,
					
					new MenuItem{
						Header="go to b1",
						Command =GlobalCommand.GoToPageCommand,
						CommandParameter = nameof(B1)
					}
		});
		}
	}
}
