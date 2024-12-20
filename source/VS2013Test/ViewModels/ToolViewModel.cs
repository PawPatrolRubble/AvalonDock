namespace VS2013Test.ViewModels
{
	public class ToolViewModel : PaneViewModel
	{
		#region fields
		private bool _isVisible = true;
		#endregion fields

		#region constructor
		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="name"></param>
		public ToolViewModel(string name)
		{
			Name = name;
			Title = name;
		}

		public ToolViewModel()
		{
			
		}

		#endregion constructor

		#region Properties
		public string Name { get; set; }

		public bool IsVisible
		{
			get => _isVisible;
			set
			{
				if (_isVisible != value)
				{
					_isVisible = value;
					RaisePropertyChanged(nameof(IsVisible));
				}
			}
		}
		#endregion Properties
	}
}
