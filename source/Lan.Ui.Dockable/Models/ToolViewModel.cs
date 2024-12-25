namespace Lan.Ui.Dockable.Models;

public class ToolViewModel : PanelViewModel
{
	public string Name { get; set; }

	private bool _isVisible;

	public bool IsVisible
	{
		get => _isVisible;
		set { SetProperty(ref _isVisible, value); }
	}
}

public class RightPaneViewModel : ToolViewModel
{

}