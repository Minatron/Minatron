using System;
using System.Runtime.InteropServices;

namespace Lacross.OS
{
	public sealed class WinUser
	{
		#if WINCE
		[DllImport("coredll")]
		#endif
		#if WIN32
        [DllImport("user32.dll")]
		#endif
		/// <summary>
		/// Ёта функци€ извлекает handle окна, если имеетс€ в наличии,
		/// захватывает ввод мышью или стилуса.
		/// “олько одно окно можно захватить дл€ ввода мыши или стилуса.		
		/// </summary>
		/// <returns> ”спешна€ работа , возвращает handle захваченного окна , асоциированного с текущим потоком
		/// NULL - указывает, что никакое окно в текущем потоке не захватило мышь</returns>
		extern public static IntPtr GetCapture();
	}
}
