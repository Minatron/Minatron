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
		/// ��� ������� ��������� handle ����, ���� ������� � �������,
		/// ����������� ���� ����� ��� �������.
		/// ������ ���� ���� ����� ��������� ��� ����� ���� ��� �������.		
		/// </summary>
		/// <returns> �������� ������ , ���������� handle ������������ ���� , ��������������� � ������� �������
		/// NULL - ���������, ��� ������� ���� � ������� ������ �� ��������� ����</returns>
		extern public static IntPtr GetCapture();
	}
}
