using Prism.Commands;
using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Linq;
using Lan.Ui.Dockable.Models;

namespace Lan.Ui.DockableApp.ViewModels
{
	public class B2ViewModel : BottomViewModel
	{
		public B2ViewModel()
		{
			Title = nameof(B2ViewModel);
			CanHide = true;
		}
	}
}
