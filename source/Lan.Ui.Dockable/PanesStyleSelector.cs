using System.Windows;
using System.Windows.Controls;
using Lan.Ui.Dockable.Models;

namespace Lan.Ui.Dockable
{
	public class PanesStyleSelector : StyleSelector
	{
		public Style ToolStyle
		{
			get;
			set;
		}

		public Style FileStyle
		{
			get;
			set;
		}


		public Style MyDocumentStyle { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			if (item is ToolViewModel)
				return ToolStyle;

			if (item is FrameworkElement page && page.DataContext.GetType().IsAssignableTo(typeof(DocumentViewModel)))
			{
				return MyDocumentStyle;
			}

			return base.SelectStyle(item, container);
		}
	}
}
