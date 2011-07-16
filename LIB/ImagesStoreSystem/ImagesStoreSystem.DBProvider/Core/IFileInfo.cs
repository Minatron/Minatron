using System;
using System.IO;

namespace ImagesStoreSystem.DBProvider.Core
{
    public enum FileInfoType : int
    {
        Default = 0,
        QuickView = 1
    }

	public interface IFileInfo
	{
		FileInfoType TypeInfo { get; set; }
		long Size { get; }
		string Name { get; set; }
		string Description { get; set; }
		DateTime Time { get; }
	}

	public interface INewFileInfo : IFileInfo
    {
        string SourcePath { get; }        
    }
}
