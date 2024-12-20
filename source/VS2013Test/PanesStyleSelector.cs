using System.Windows;
using System.Windows.Controls;
using VS2013Test.ViewModels;

namespace VS2013Test
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

			if (item is FileViewModel)
				return FileStyle;


			if (item is FrameworkElement page && page.DataContext.GetType().IsAssignableTo(typeof(FileViewModel)))
			{
				return MyDocumentStyle;
			}

			return base.SelectStyle(item, container);
		}
	}
}
