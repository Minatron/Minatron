using System;
using Microsoft.Practices.Composite.Presentation.Events;

namespace ImageStoreSystem.Infrastructure
{
	public class CreateStationEvent : CompositePresentationEvent<EditObjectWithNumberEventArgs> { }
	public class CreateLevelEvent : CompositePresentationEvent<EditEventArgs> { }
	public class CreateSensorEvent : CompositePresentationEvent<EditEventArgs> { }
	public class CreateStorageEvent : CompositePresentationEvent<EditEventArgs> { }
	public class CreateAttributeTypeEvent : CompositePresentationEvent<EditEventArgs> { }
	public class CreateSatelliteEvent : CompositePresentationEvent<EditObjectWithNumberEventArgs> { }

	public class EditEventArgs
	{
		public object Object { get; protected set; }
		public object Repository { get; protected set; }
		public Action<object> OnEditAccepted { get; protected set; }

		public EditEventArgs(object obj, object repo, Action<object> onEditAccepted)
		{
			Object = obj;
			Repository = repo;
			OnEditAccepted = onEditAccepted;
		}

		public EditEventArgs(object obj = null, object repo = null) : this(obj, repo, o => { }) { }
		public EditEventArgs(Action<object> action) : this(null, null, action) { }
	}

	public class EditObjectWithNumberEventArgs : EditEventArgs
	{
		public long CatalogNumber { get; protected set; }

		public EditObjectWithNumberEventArgs(object obj, object repo, Action<object> onEditAccepted, long number)
			: base(obj, repo, onEditAccepted)
		{
			CatalogNumber = number;
		}

		public EditObjectWithNumberEventArgs(object obj = null, object repo = null, long number = 0) : this(obj, repo, o => { }, number) { }
		public EditObjectWithNumberEventArgs(Action<object> action, long number = 0) : this(null, null, action, number) { }
	}
}
