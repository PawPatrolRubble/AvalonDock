using System.Windows.Controls;
using System.Windows.Media;
using Prism.Mvvm;

namespace Lan.Ui.Dockable.Models;

public class PanelViewModel : BindableBase
{
	private string _contentId;
	private bool _isActive;
	private bool _isSelected;
	private string _title;


	public string Title
	{
		get => _title;
		set => SetProperty(ref _title, value);
	}

	public string ContentId
	{
		get => _contentId;
		set => SetProperty(ref _contentId, value);
	}

	public bool IsSelected
	{
		get => _isSelected;
		set => SetProperty(ref _isSelected, value);
	}

	public bool IsActive
	{
		get => _isActive;
		set => SetProperty(ref _isActive, value);
	}


	private bool _canClose;

	public bool CanClose
	{
		get => _canClose;
		set { SetProperty(ref _canClose, value); }
	}


	public ImageSource IconSource { get; protected set; }
}