using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using AvalonDock;
using AvalonDock.Layout;
using ImTools;
using Lan.AvalonDock.PrismTest.ViewModels;
using Prism.Regions;

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
			// Ensure the layout structure exists
			if (regionTarget.Layout == null)
				regionTarget.Layout = new LayoutRoot();

			// Ensure we have document and tool panes
			var layoutRoot = regionTarget.Layout;
			var documentPane = layoutRoot.RootPanel.Children
				.OfType<LayoutPanel>()
				.FirstOrDefault().Children.OfType<LayoutDocumentPane>().FirstOrDefault();
			var toolPane = layoutRoot.RootPanel.Children
				.OfType<LayoutAnchorablePaneGroup>()
				.FirstOrDefault().Children.OfType<LayoutAnchorablePane>().FirstOrDefault();

			if (documentPane == null)
			{
				documentPane = new LayoutDocumentPane();
				layoutRoot.RootPanel.Children.Add(documentPane);
			}
			if (toolPane == null)
			{
				toolPane = new LayoutAnchorablePane();
				layoutRoot.RootPanel.Children.Add(toolPane);
			}

			// Handle adding views to the DockingManager region
			region.Views.CollectionChanged += (s, e) =>
			{
				if (e.Action == NotifyCollectionChangedAction.Add)
				{
					foreach (FrameworkElement view in e.NewItems)
					{

						var source = regionTarget.AnchorablesSource;

						var viewModel = view.DataContext;

						if (viewModel is IToolViewModel)
						{
							var anchorable = new LayoutAnchorable
							{
								Content = view,
								Title = (viewModel as IDockable)?.Title ?? "Tool"
							};
							toolPane.Children.Add(anchorable);
							anchorable.IsActive = true;
						}
						else if (viewModel is IDocumentViewModel)
						{
							var document = new LayoutDocument
							{
								Content = view,
								Title = (viewModel as IDockable)?.Title ?? "Document"
							};
							documentPane.Children.Add(document);
							document.IsActive = true;
						}
					}
				}
			};
		}

		protected override IRegion CreateRegion() => new AllActiveRegion();
	}

}
