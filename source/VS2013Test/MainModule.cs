using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using VS2013Test.ViewModels;
using VS2013Test.Views;

namespace VS2013Test
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
			containerRegistry.RegisterForNavigation<SolutionA, SolutionAViewModel>();
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
		}
	}

}
