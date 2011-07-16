using Microsoft.Practices.Composite.Presentation.Events;
using System;

namespace ImageStoreSystem.Infrastructure
{
	public class EditImageEvent : CompositePresentationEvent<EditImageEventArgs>
	{
	}

	public class ViewDetailsOfImageEvent : CompositePresentationEvent<EditEventArgs>
	{
	}

	public class EditImageEventArgs : EditEventArgs
	{
		public object ReceiveSession { get; protected set; }

		public EditImageEventArgs(object obj, object repo, object receiveSession, Action<object> onEditAccepted)
			: base(obj, repo, onEditAccepted)
		{
			ReceiveSession = receiveSession;
		}

		public EditImageEventArgs(object obj = null, object repo = null, object receiveSession = null)
			: this(obj, repo, receiveSession, o => { }) { }
	}
}
