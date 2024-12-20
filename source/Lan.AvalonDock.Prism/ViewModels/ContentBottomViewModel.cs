using Prism.Commands;
using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lan.AvalonDock.PrismTest.ViewModels
{
	public class ContentBottomViewModel : BindableBase,IBottomViewModel
	{
		public ContentBottomViewModel()
		{
			Title = "Bottom1";
		}

		public string Title { get; }
	}
}
