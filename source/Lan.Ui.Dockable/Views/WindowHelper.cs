using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

	using System;
	using System.Runtime.InteropServices;
	using System.Windows;
	using System.Windows.Interop;

namespace Lan.Ui.Dockable.Views
{

	public static class WindowHelper
	{
		const int SWP_NOSIZE = 0x0001;
		const int SWP_NOMOVE = 0x0002;
		const int HWND_BOTTOM = 1;
		const uint SWP_NOACTIVATE = 0x0010;

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			int X, int Y, int cx, int cy, uint uFlags);

		public static void SendToBack(Window window)
		{
			var hwnd = new WindowInteropHelper(window).Handle;
			SetWindowPos(hwnd, (IntPtr)HWND_BOTTOM, 0, 0, 0, 0,
				SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
		}
	}

}
