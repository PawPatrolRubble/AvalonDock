using Prism.Mvvm;

namespace Lan.AvalonDock.PrismTest.ViewModels
{
	public class FileViewAViewModel : BindableBase,IDocumentViewModel
	{
		public FileViewAViewModel()
		{
			CanClose = false;
		}

		public string Title { get; } = "this is a document title";
		public bool CanClose { get; }
	}
}
