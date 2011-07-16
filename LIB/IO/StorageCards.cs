using System;
using System.IO;
using System.Collections;


namespace Lacross.IO
{
	public sealed class StorageCards
	{
		/// <summary>
		/// јтрибут файла соответсвующий карте пам€ти
		/// </summary>
		public const FileAttributes AttributeStorageCard = FileAttributes.Directory | FileAttributes.Temporary;
		/// <summary>
		/// ¬озрощает список имен активных карт пам€ти
		/// </summary>
		/// <returns>масив имен</returns>
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
		/// ¬озрощает список активных карт пам€ти
		/// </summary>
		/// <returns>масив карт пам€ти</returns>
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
		/// ¬озрощает информацию о свободном месте 
		/// </summary>
		/// <param name="directoryName">им€</param>
		/// <param name="FreeBytesAvailable">всего свободных байт доступных дл€ пользовани€</param>
		/// <param name="TotalBytes">всего байт на диске</param>
		/// <param name="TotalFreeBytes">всего свободных байт</param>
		public static void GetFreeSpace(string Name,ref long FreeBytesAvailable,ref long TotalBytes,ref long TotalFreeBytes)
		{
			Lacross.OS.WinBase.GetDiskFreeSpace(Name,ref FreeBytesAvailable,ref TotalBytes,ref TotalFreeBytes);
		}
	}
}
