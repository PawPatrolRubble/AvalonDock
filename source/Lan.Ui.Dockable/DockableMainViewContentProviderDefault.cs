using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AvalonDock.Layout;

namespace Lan.Ui.Dockable;

public class DockableMainViewContentProviderDefault : IDockableMainViewContentProvider
{
	public LayoutAnchorablePane RightPane { get; private set; }
	public LayoutAnchorablePane BottomPane { get; private set; }
	public LayoutDocumentPane DocumentPane { get; private set; }

	public ObservableCollection<MenuItem> Menus { get; set; }

	public LayoutRoot Layout { get; set; }

	public DockableMainViewContentProviderDefault()
	{
		Initialize();
		Menus = new ObservableCollection<MenuItem>();
		
	}

	public void Initialize()
	{
		Layout = new LayoutRoot
		{
			RootPanel = new LayoutPanel
			{
				Orientation = Orientation.Horizontal,
				DockWidth = new GridLength(1, GridUnitType.Star)
			}
		};





		var leftPanel = new LayoutPanel()
		{
			Orientation = Orientation.Vertical
		};


		DocumentPane = new LayoutDocumentPane();
		BottomPane = new LayoutAnchorablePane { Name = "OutputPane", DockHeight = new GridLength(200)};
		leftPanel.Children.Add(DocumentPane);
		leftPanel.Children.Add(BottomPane);

		RightPane = new LayoutAnchorablePane()
		{
			DockWidth = new GridLength(150)
		};
		// Add DocumentPane and BottomPane to the left panel
		Layout.RootPanel.Children.Add(leftPanel);
		Layout.RootPanel.Children.Add(RightPane);


		// Create the right anchorable pane group
		//var rightPaneGroup = new LayoutAnchorablePaneGroup { DockWidth = new System.Windows.GridLength(200), Orientation = Orientation.Vertical };
		//RightPane = new LayoutAnchorablePane { Name = "PropertiesPane" };
		//rightPaneGroup.Children.Add(RightPane);

		// Add left panel and right pane group to the main panel
		//mainPanel.Children.Add(leftPanel);
		//mainPanel.Children.Add(rightPaneGroup);

		// Finally, add the main panel to the layout root
	
	}

}