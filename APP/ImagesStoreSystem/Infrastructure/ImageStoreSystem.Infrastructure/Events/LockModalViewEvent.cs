using Microsoft.Practices.Composite.Presentation.Events;

namespace ImageStoreSystem.Infrastructure
{
	public class LockModalViewEvent : CompositePresentationEvent<object> { }
	public class UnLockModalViewEvent : CompositePresentationEvent<object> { }
}
