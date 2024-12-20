using Lan.AvalonDock.PrismTest.ViewModels;
using Lan.AvalonDock.PrismTest.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Lan.AvalonDock.PrismTest
{
	public class MainModule : IModule
	{
		private readonly IRegionManager _regionManager;

		public MainModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
		
			containerRegistry.RegisterForNavigation<FileViewA, FileViewAViewModel>();
			containerRegistry.RegisterForNavigation<Page2, Page2ViewModel>();
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
		}
	}

}
