using Prism.Commands;
using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lan.AvalonDock.PrismTest.ViewModels
{
	public class Bottom2ViewModel : BindableBase,IBottomViewModel
	{
		public Bottom2ViewModel()
		{
			Title ="Bottom2";
		}

		public string Title { get; }
	}
}
