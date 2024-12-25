using Prism.Commands;
using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lan.AvalonDock.PrismTest.ViewModels
{
	public class View2ViewModel : BindableBase, IDocumentViewModel
	{
		public View2ViewModel()
		{
			Title = nameof(View2ViewModel);
			CanClose = true;
		}

		public string Title { get; }
		public bool CanClose { get; }
	}
}
