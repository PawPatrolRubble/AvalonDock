namespace Lan.Ui.Dockable.Models;

public class BottomViewModel : ToolViewModel
{
	private bool _canHide;

	public bool CanHide
	{
		get => _canHide;
		set { SetProperty(ref _canHide, value); }
	}

}