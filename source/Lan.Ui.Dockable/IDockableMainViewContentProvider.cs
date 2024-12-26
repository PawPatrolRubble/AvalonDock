using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using AvalonDock.Layout;

namespace Lan.Ui.Dockable
{
	public interface IDockableMainViewContentProvider
	{
		ObservableCollection<MenuItem> Menus { get; }
		LayoutAnchorablePane RightPane { get; }
		LayoutAnchorablePane BottomPane { get; }
		LayoutDocumentPane DocumentPane { get; }
		LayoutRoot Layout { get; set; }

		/// <summary>
		/// this is going to create the layout for dockingmanager
		/// </summary>
		void Initialize();
	}
}
