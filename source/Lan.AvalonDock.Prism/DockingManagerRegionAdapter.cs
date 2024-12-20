using System.Collections.Generic;
using System;
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

		#region Overrides

		protected override IRegion CreateRegion()
		{
			return new AllActiveRegion();
		}

		protected override void Adapt(IRegion region, DockingManager regionTarget)
		{
			region.Views.CollectionChanged += delegate (
				Object sender, NotifyCollectionChangedEventArgs e)
			{
				this.OnViewsCollectionChanged(sender, e, region, regionTarget);
			};

			regionTarget.DocumentClosed += delegate (
							Object sender, DocumentClosedEventArgs e)
			{
				this.OnDocumentClosedEventArgs(sender, e, region);
			};
		}

		#endregion  //Overrides


		#region Event Handlers

		/// <summary>
		/// Handles the NotifyCollectionChangedEventArgs event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event.</param>
		/// <param name="region">The region.</param>
		/// <param name="regionTarget">The region target.</param>
		void OnViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e, IRegion region, DockingManager regionTarget)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (FrameworkElement item in e.NewItems)
				{
					UIElement view = item as UIElement;

					if (view != null)
					{
						//Create a new layout document to be included in the LayoutDocuemntPane (defined in xaml)
						LayoutDocument newLayoutDocument = new LayoutDocument();
						//Set the content of the LayoutDocument
						newLayoutDocument.Content = item;

						var viewModel = (IDockable)item.DataContext;

						if (viewModel != null)
						{
							//All my viewmodels have properties DisplayName and IconKey
							newLayoutDocument.Title = viewModel.Title;
							//GetImageUri is custom made method which gets the icon for the LayoutDocument
							//newLayoutDocument.IconSource = this.GetImageUri(viewModel.IconKey);
						}

						//Store all LayoutDocuments already pertaining to the LayoutDocumentPane (defined in xaml)
						List<LayoutDocument> oldLayoutDocuments = new List<LayoutDocument>();

						//Get the current ILayoutDocumentPane ... Depending on the arrangement of the views this can be either 
						//a simple LayoutDocumentPane or a LayoutDocumentPaneGroup
						//ILayoutDocumentPane currentILayoutDocumentPane = (ILayoutDocumentPane)regionTarget.Layout.RootPanel.Children.OfType<ILayoutControl>();

						LayoutDocumentPane documentPane = (LayoutDocumentPane)((LayoutPanel)
							regionTarget.Layout.RootPanel.Children[0]).Children[0]; 

						LayoutAnchorablePane solutionPane =
							(LayoutAnchorablePane)((LayoutAnchorablePaneGroup)regionTarget.Layout.RootPanel.Children.FirstOrDefault(x =>
								x.GetType() == typeof(LayoutAnchorablePaneGroup))).Children[0];
						
						LayoutAnchorablePane bottomPane =
							(LayoutAnchorablePane)((LayoutAnchorablePaneGroup)((LayoutPanel)
								regionTarget.Layout.RootPanel.Children[0]).Children[1]).Children[0];


						if (viewModel.GetType().IsAssignableTo(typeof(IToolViewModel)))
						{
							solutionPane.Children.Add(new LayoutAnchorable()
							{
								Content = item,
								Title = viewModel.Title,
							});
						}

						if (viewModel.GetType().IsAssignableTo(typeof(IDocumentViewModel)))
						{
							documentPane.Children.Add(new LayoutDocument()
							{
								Title = viewModel.Title,
								Content = item
							});
						}

						if (viewModel.GetType().IsAssignableTo(typeof(IBottomViewModel)))
						{
							bottomPane.Children.Add(new LayoutAnchorable()
							{
								Content = item,
								Title = viewModel.Title
							});

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
				}
			}
		}

		/// <summary>
		/// Handles the DocumentClosedEventArgs event raised by the DockingNanager when
		/// one of the LayoutContent it hosts is closed.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event.</param>
		/// <param name="region">The region.</param>
		void OnDocumentClosedEventArgs(object sender, DocumentClosedEventArgs e, IRegion region)
		{
			region.Remove(e.Document.Content);
		}

		#endregion  //Event handlers
	}

}
