using System;
using System.IO;
using System.Collections;


namespace Lacross.IO
{
	public sealed class StorageCards
	{
		/// <summary>
		/// ������� ����� �������������� ����� ������
		/// </summary>
		public const FileAttributes AttributeStorageCard = FileAttributes.Directory | FileAttributes.Temporary;
		/// <summary>
		/// ��������� ������ ���� �������� ���� ������
		/// </summary>
		/// <returns>����� ����</returns>
		public static string[] GetNames()
		{
			ArrayList scards = new ArrayList();
			#if WINCE
			DirectoryInfo root = new DirectoryInfo(@"\");
			foreach(DirectoryInfo di in root.GetDirectories() )
			{
				if ( (di.Attributes & AttributeStorageCard) == AttributeStorageCard ) scards.Add(di.Name);
			}
			#endif
			return (string[])scards.ToArray(typeof(string));
		}

		/// <summary>
		/// ��������� ������ �������� ���� ������
		/// </summary>
		/// <returns>����� ���� ������</returns>
		public static DirectoryInfo[] GetStorageCards()
		{
			ArrayList scards = new ArrayList();
			#if WINCE
			DirectoryInfo root = new DirectoryInfo(@"\");
			
			foreach(DirectoryInfo di in root.GetDirectories() )
			{
				if ( (di.Attributes & AttributeStorageCard) == AttributeStorageCard ) scards.Add(di.Name);
			}
			#endif
			return (DirectoryInfo[])scards.ToArray(Type.GetType("System.IO.DirectoryInfo"));
		}
	
	
		/// <summary>
		/// ��������� ���������� � ��������� ����� 
		/// </summary>
		/// <param name="directoryName">���</param>
		/// <param name="FreeBytesAvailable">����� ��������� ���� ��������� ��� �����������</param>
		/// <param name="TotalBytes">����� ���� �� �����</param>
		/// <param name="TotalFreeBytes">����� ��������� ����</param>
		public static void GetFreeSpace(string Name,ref long FreeBytesAvailable,ref long TotalBytes,ref long TotalFreeBytes)
		{
			Lacross.OS.WinBase.GetDiskFreeSpace(Name,ref FreeBytesAvailable,ref TotalBytes,ref TotalFreeBytes);
		}
	}
}
