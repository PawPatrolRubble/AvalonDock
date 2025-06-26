#nullable enable
using System.Collections.Specialized;
using System.Windows;
using AvalonDock;
using AvalonDock.Layout;
using Lan.Ui.Dockable.Models;
using Prism.Regions;

namespace Lan.Ui.Dockable;

public class DockingManagerRegionAdapter : RegionAdapterBase<DockingManager>
{
	private readonly IDockableMainViewContentProvider _dockableMainViewContentProvider;

	public DockingManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory,
		IDockableMainViewContentProvider dockableMainViewContentProvider)
		: base(regionBehaviorFactory)
	{
		_dockableMainViewContentProvider = dockableMainViewContentProvider;
		_documentPane = _dockableMainViewContentProvider.DocumentPane;
		_bottomPane = _dockableMainViewContentProvider.BottomPane;
		_rightPane = _dockableMainViewContentProvider.RightPane;
		_leftPane = _dockableMainViewContentProvider.LeftPane;
	}

	#region Overrides

	protected override IRegion CreateRegion()
	{
		return new AllActiveRegion();
	}

	protected override void Adapt(IRegion region, DockingManager regionTarget)
	{
		region.Views.CollectionChanged += delegate(
			object sender, NotifyCollectionChangedEventArgs e)
		{
			OnViewsCollectionChanged(sender, e, region, regionTarget);
		};


		regionTarget.DocumentClosed += delegate(
			object sender, DocumentClosedEventArgs e)
		{
			OnDocumentClosedEventArgs(sender, e, region);
		};


		regionTarget.LayoutFloatingWindowControlClosed += (s, e) => { ; };


		regionTarget.AnchorableClosed += (s, e) => { OnAnchorableClosed(s, e, region); };

		regionTarget.AnchorableHidden += (s, e) =>
		{
			e.Anchorable.Close();
			;
		};
	}

	#endregion //Overrides


	#region Event Handlers

	private readonly LayoutDocumentPane? _documentPane;
	private readonly LayoutAnchorablePane? _rightPane;
	private readonly LayoutAnchorablePane? _bottomPane;
	private readonly LayoutAnchorablePane? _leftPane;

	/// <summary>
	///     Handles the NotifyCollectionChangedEventArgs event.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event.</param>
	/// <param name="region">The region.</param>
	/// <param name="regionTarget">The region target.</param>
	private void OnViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e, IRegion region,
		DockingManager regionTarget)
	{
		if (e.Action == NotifyCollectionChangedAction.Add)
			foreach (FrameworkElement view in e.NewItems)
				if (view != null)
					switch (view.DataContext)
					{
						case RightPaneViewModel toolViewModel:
							_rightPane.Children.Add(new LayoutAnchorable
							{
								Content = view,
								Title = toolViewModel.Title
							});
							break;
						case DocumentViewModel documentViewModel:

							_documentPane.Children.Add(new LayoutDocument
							{
								Title = documentViewModel.Title,
								Content = view,
								CanClose = documentViewModel.CanClose
							});
							break;

						case BottomViewModel bottomViewModel:

							_bottomPane.Children.Add(new LayoutAnchorable
							{
								Content = view,
								Title = bottomViewModel.Title,
								CanHide = bottomViewModel.CanHide
							});
							break;
						case LeftPaneViewModel leftPaneViewModel:
							_leftPane.Children.Add(new LayoutAnchorable()
							{
								Content = view,
								Title = leftPaneViewModel.Title,
								CanClose = leftPaneViewModel.CanClose
							});
							break;
					}
		//if (currentILayoutDocumentPane.GetType() == typeof(LayoutDocumentPaneGroup))
		//{
		//	//If the current ILayoutDocumentPane turns out to be a group
		//	//Get the children (LayoutDocuments) of the first pane
		//	LayoutDocumentPane oldLayoutDocumentPane = (LayoutDocumentPane)currentILayoutDocumentPane.Children.ToList()[0];
		//	foreach (LayoutDocument child in oldLayoutDocumentPane.Children)
		//	{
		//		oldLayoutDocuments.Insert(0, child);
		//	}
		//}
		//else if (currentILayoutDocumentPane.GetType() == typeof(LayoutDocumentPane))
		//{
		//	//If the current ILayoutDocumentPane turns out to be a simple pane
		//	//Get the children (LayoutDocuments) of the single existing pane.
		//	foreach (LayoutDocument child in currentILayoutDocumentPane.Children)
		//	{
		//		oldLayoutDocuments.Insert(0, child);
		//	}
		//}
		////Create a new LayoutDocumentPane and inserts your new LayoutDocument
		//LayoutDocumentPane newLayoutDocumentPane = new LayoutDocumentPane();
		//newLayoutDocumentPane.InsertChildAt(0, newLayoutDocument);
		////Append to the new LayoutDocumentPane the old LayoutDocuments
		//foreach (LayoutDocument doc in oldLayoutDocuments)
		//{
		//	newLayoutDocumentPane.InsertChildAt(0, doc);
		//}
		////Traverse the visual tree of the xaml and replace the LayoutDocumentPane (or LayoutDocumentPaneGroup) in xaml
		////with your new LayoutDocumentPane (or LayoutDocumentPaneGroup)
		//if (currentILayoutDocumentPane.GetType() == typeof(LayoutDocumentPane))
		//	regionTarget.Layout.RootPanel.ReplaceChildAt(0, newLayoutDocumentPane);
		//else if (currentILayoutDocumentPane.GetType() == typeof(LayoutDocumentPaneGroup))
		//{
		//	currentILayoutDocumentPane.ReplaceChild(currentILayoutDocumentPane.Children.ToList()[0], newLayoutDocumentPane);
		//	regionTarget.Layout.RootPanel.ReplaceChildAt(0, currentILayoutDocumentPane);
		//}
		//newLayoutDocument.IsActive = true;
	}

	/// <summary>
	///     Handles the DocumentClosedEventArgs event raised by the DockingNanager when
	///     one of the LayoutContent it hosts is closed.
	/// </summary>
	/// <param name="sender">The sender</param>
	/// <param name="e">The event.</param>
	/// <param name="region">The region.</param>
	private void OnDocumentClosedEventArgs(object sender, DocumentClosedEventArgs e, IRegion region)
	{
		region.Remove(e.Document.Content);
	}


	private void OnAnchorableClosed(object sender, AnchorableClosedEventArgs args, IRegion region)
	{
		region.Remove(args.Anchorable.Content);
	}

	#endregion //Event handlers
}