using ImagesStoreSystem.DBProvider.Core;

namespace CommonObjects
{
	public interface IFieldPresenterBase<T> where T : UpdatableWithPacketObject
	{
		string Title { get; }
		bool HasValue { get; set; }
		void ApplyTo(T obj);
		void InitFor(T obj);
		bool CanRemove { get; }
		void Reset();
		bool IsCorrect { get; }
	}
}
