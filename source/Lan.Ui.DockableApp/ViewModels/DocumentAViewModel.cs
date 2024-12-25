using Prism.Commands;
using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Linq;
using Lan.Ui.Dockable.Models;

namespace Lan.Ui.DockableApp.ViewModels
{
	public class DocumentAViewModel : DocumentViewModel
	{
		public DocumentAViewModel()
		{
			Title = "test";
			CanClose = true;
		}
	}
}
