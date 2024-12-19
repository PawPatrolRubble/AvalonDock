using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VS2013Test.ViewModels;

namespace VS2013Test
{
	public interface IDockingNavigationService
	{
		ReadOnlyObservableCollection<FrameworkElement> Files { get; }
		void Add(FrameworkElement fileViewModel);
	}

	public class DockingNavigationService : IDockingNavigationService
	{
		private ReadOnlyObservableCollection<FrameworkElement> _readonyFiles;
		private ObservableCollection<FrameworkElement> _files = new ObservableCollection<FrameworkElement>();
		public ReadOnlyObservableCollection<FrameworkElement> Files
		{
			get
			{
				if (_readonyFiles == null)
					_readonyFiles = new ReadOnlyObservableCollection<FrameworkElement>(_files);

				return _readonyFiles;
			}
		}

		public void Add(FrameworkElement fileViewModel)
		{
			_files.Add(fileViewModel);
		}
	}
}
