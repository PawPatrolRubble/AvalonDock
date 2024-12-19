using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using AvalonDock;
using AvalonDock.Layout;
using DryIoc;
using ImTools;
using Prism.Ioc;
using Prism.Regions;
using VS2013Test;

namespace Lan.AvalonDock.PrismTest
{
	public class DockingManagerRegionAdapter : RegionAdapterBase<DockingManager>
	{
		public DockingManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
			: base(regionBehaviorFactory)
		{
		}

		protected override void Adapt(IRegion region, DockingManager regionTarget)
		{

			var service = ContainerLocator.Current.Resolve<IDockingNavigationService>();

			// Handle adding views to the DockingManager region
			region.Views.CollectionChanged += (s, e) =>
			{
				if (e.Action == NotifyCollectionChangedAction.Add)
				{
					foreach (FrameworkElement view in e.NewItems)
					{

						service.Add(view);
			

						//if (viewModel is IToolViewModel)
						//{
						//	var anchorable = new LayoutAnchorable
						//	{
						//		Content = view,
						//		Title = (viewModel as IDockable)?.Title ?? "Tool"
						//	};
						//	toolPane.Children.Add(anchorable);
						//	anchorable.IsActive = true;
						//}
						//else if (viewModel is panel)
						//{
						//	var document = new LayoutDocument
						//	{
						//		Content = view,
						//		Title = (viewModel as IDockable)?.Title ?? "Document"
						//	};
						//	documentPane.Children.Add(document);
						//	document.IsActive = true;
						//}
					}
				}
			};
		}

		protected override IRegion CreateRegion() => new AllActiveRegion();
	}

}
