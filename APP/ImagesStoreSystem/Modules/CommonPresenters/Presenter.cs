using System.ComponentModel;

namespace CommonPresenters
{
	public abstract class Presenter : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
