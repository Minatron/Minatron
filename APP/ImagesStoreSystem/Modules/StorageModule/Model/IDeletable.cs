using System.ComponentModel;

namespace StorageModule.Model
{
	public interface IDeletable : INotifyPropertyChanged
	{
		bool IsDeleted { get; set; }
	}
}
