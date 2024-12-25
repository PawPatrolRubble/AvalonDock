using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AvalonDock.Layout;

namespace Lan.Ui.Dockable;

public class DockableMainViewContentProvider : IDockableMainViewContentProvider
{
	public LayoutAnchorablePane RightPane { get; private set; }
	public LayoutAnchorablePane BottomPane { get; private set; }
	public LayoutDocumentPane DocumentPane { get; private set; }

	public ObservableCollection<MenuItem> Menus { get; set; }

	public LayoutRoot Layout { get; set; }

	public DockableMainViewContentProvider()
	{
		Initialize();
		Menus = new ObservableCollection<MenuItem>();
		
	}

	private void Initialize()
	{
		Layout = new LayoutRoot();

		// Create the main horizontal panel
		var mainPanel = new LayoutPanel { Orientation = Orientation.Horizontal };

		// Create the left vertical panel
		var leftPanel = new LayoutPanel { Orientation = Orientation.Vertical, DockWidth = new GridLength(1, GridUnitType.Star)};
		
		DocumentPane = new LayoutDocumentPane();
		var bottomPanel = new LayoutAnchorablePaneGroup { DockHeight = new System.Windows.GridLength(128), Orientation = Orientation.Horizontal };

		// Create the child anchorable panes for the bottom panel
		BottomPane = new LayoutAnchorablePane { Name = "OutputPane" };

		bottomPanel.Children.Add(BottomPane);

		// Add DocumentPane and BottomPane to the left panel
		leftPanel.Children.Add(DocumentPane);
		leftPanel.Children.Add(BottomPane);



		// Create the right anchorable pane group
		var rightPaneGroup = new LayoutAnchorablePaneGroup { DockWidth = new System.Windows.GridLength(200), Orientation = Orientation.Vertical };
		RightPane = new LayoutAnchorablePane { Name = "PropertiesPane" };
		rightPaneGroup.Children.Add(RightPane);

		// Add left panel and right pane group to the main panel
		mainPanel.Children.Add(leftPanel);
		mainPanel.Children.Add(rightPaneGroup);

		// Finally, add the main panel to the layout root
		Layout.RootPanel.Children.Add(mainPanel);
	}

}