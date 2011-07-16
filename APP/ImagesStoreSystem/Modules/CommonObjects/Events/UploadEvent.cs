using ImagesStoreSystem.DBProvider.Core;
using Microsoft.Practices.Composite.Presentation.Events;

namespace CommonObjects
{
	public class UploadEvent<T> : CompositePresentationEvent<T> where T : UpdatableWithPacketObject
	{
	}
}
