using System.ComponentModel;

namespace Band.Module.WeighData.Presenters
{
    public class PageInfoPresenter : INotifyPropertyChanged
    {
        public const int CAPACITY = 9;

        int _index = 0;

        public PageInfoPresenter()
        {
            IsEmpty = true;
            HasNextPage = false;
        }

        public int Number
        {
            get
            {
                return Index + 1;
            }
        }
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (value < 0) value = 0;
                if ((value > _index) && (!HasNextPage)) value = _index;
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged("Number");
                    OnPropertyChanged("HasPrevPage");
                }
            }
        }
        public bool IsEmpty { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool HasPrevPage
        {
            get
            {
                return _index > 0;
            }
        }
     
        public void setPropertyForCountObjects(int count)
        {
            HasNextPage = (count == CAPACITY) && (count > 0);
            IsEmpty = (Index == 0) && (count == 0);
            OnPropertyChanged("HasNextPage");
            OnPropertyChanged("IsEmpty");
        }

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
