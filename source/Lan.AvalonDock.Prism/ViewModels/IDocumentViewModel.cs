namespace Lan.AvalonDock.PrismTest.ViewModels
{
    public interface IDocumentViewModel : IDockable
    {
		bool CanClose { get; }
    }
} 