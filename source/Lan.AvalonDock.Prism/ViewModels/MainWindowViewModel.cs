using System;
using System.Collections.Generic;
using System.Windows;
using AvalonDock.Themes;
using DryIoc;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using VS2013Test.ViewModels;

namespace Lan.AvalonDock.PrismTest.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private readonly IRegionManager _regionManager;
		private string _title = "Prism Application";
		private Tuple<string, Theme> _selectedTheme;
		private FileViewModel _activeDocument;
		private ErrorViewModel _errors;
		private PropertiesViewModel _props;
		private ExplorerViewModel _explorer;
		private OutputViewModel _output;
		private GitChangesViewModel _git;
		private ToolboxViewModel _toolbox;
		private ToolViewModel[] _tools;

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public List<Tuple<string, Theme>> Themes { get; set; } = new List<Tuple<string, Theme>>
		{
			new Tuple<string, Theme>(nameof(Vs2013DarkTheme),new Vs2013DarkTheme()),
			new Tuple<string, Theme>(nameof(Vs2013LightTheme),new Vs2013LightTheme()),
			new Tuple<string, Theme>(nameof(Vs2013BlueTheme),new Vs2013BlueTheme())
		};

		public Tuple<string, Theme> SelectedTheme
		{
			get { return _selectedTheme; }
			set
			{
				_selectedTheme = value;
				SwitchExtendedTheme();
				RaisePropertyChanged(nameof(SelectedTheme));
			}
		}

		private void SwitchExtendedTheme()
		{
			switch (_selectedTheme.Item1)
			{
				case "Vs2013DarkTheme":
					Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MLib;component/Themes/DarkTheme.xaml");
					Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/VS2013Test;component/Themes/DarkBrushsExtended.xaml");
					break;
				case "Vs2013LightTheme":
					Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MLib;component/Themes/LightTheme.xaml");
					Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/VS2013Test;component/Themes/LightBrushsExtended.xaml");
					break;
				case "Vs2013BlueTheme":
					//TODO: Create new color resources for blue theme
					Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MLib;component/Themes/LightTheme.xaml");
					Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/VS2013Test;component/Themes/BlueBrushsExtended.xaml");
					break;
				default:
					break;

			}
		}

		public IEnumerable<ToolViewModel> Tools
		{
			get
			{
				if (_tools == null)
					_tools = new ToolViewModel[] { Explorer, Props, Errors, Output, Git, Toolbox };
				return _tools;
			}
		}

		public ExplorerViewModel Explorer
		{
			get
			{
				if (_explorer == null)
					_explorer = new ExplorerViewModel();

				return _explorer;
			}
		}

		public PropertiesViewModel Props
		{
			get
			{
				if (_props == null)
					_props = new PropertiesViewModel();

				return _props;
			}
		}

		public ErrorViewModel Errors
		{
			get
			{
				if (_errors == null)
					_errors = new ErrorViewModel();

				return _errors;
			}
		}

		public OutputViewModel Output
		{
			get
			{
				if (_output == null)
					_output = new OutputViewModel();

				return _output;
			}
		}

		public GitChangesViewModel Git
		{
			get
			{
				if (_git == null)
					_git = new GitChangesViewModel();

				return _git;
			}
		}

		public ToolboxViewModel Toolbox
		{
			get
			{
				if (_toolbox == null)
					_toolbox = new ToolboxViewModel();

				return _toolbox;
			}
		}


		public DelegateCommand<object> GoToCommand { get; }

		private void GoTo(object pageName)
		{
			if (pageName is string viewName)
			{
				_regionManager.RequestNavigate("ContentRegion", viewName);
			}
		}

		public MainWindowViewModel(IRegionManager regionManager)
		{
			_regionManager = regionManager;
			GoToCommand = new DelegateCommand<object>(GoTo);
		}
	}
}
