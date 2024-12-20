using Prism.Mvvm;

namespace Lan.AvalonDock.PrismTest.ViewModels
{
	public class FileViewAViewModel : BindableBase,IDocumentViewModel
	{
		public FileViewAViewModel()
		{
			
		}

		public string Title { get; } = "this is a document title";
	}
}
