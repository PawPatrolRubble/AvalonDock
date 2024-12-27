using System;
using System.ComponentModel;
using System.Windows;
using Lan.Ui.Dockable.ViewModels;

namespace Lan.Ui.Dockable.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			// Show a confirmation message box
			MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

			// Check the user's response
			if (result == MessageBoxResult.No)
			{
				// Cancel the closing event
				e.Cancel = true;
			}
		}


		private void MinimizeClick(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void MaximizeClick(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Maximized;
		}

		private void RestoreDownClick(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Normal;
		}

		private void CloseClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void WindowStateChanged(object sender, EventArgs e)
		{
			SetCaptionHeight();
		}

		private void HeaderSizeChanged(object sender, SizeChangedEventArgs e)
		{
			SetCaptionHeight();
		}

		private void SetCaptionHeight()
		{
			switch (WindowState)
			{
				case WindowState.Normal:
					chrome.CaptionHeight = header.ActualHeight + BorderThickness.Top - chrome.ResizeBorderThickness.Top;
					break;
				case WindowState.Maximized:
					chrome.CaptionHeight = header.ActualHeight - BorderThickness.Top;
					break;
			}
		}
	}
}
