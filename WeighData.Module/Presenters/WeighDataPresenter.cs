using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Band.Client.Infrastructure;
using Band.Client.Infrastructure.Events;
using Band.Client.Infrastructure.Storage;
using Band.OLD;
using Band.Storage;
using Band.Storage.Minatron;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using Weigh = Band.Storage.Minatron.WeighData;

namespace Band.Module.WeighData.Presenters
{
    public class WeighDataPresenter : INotifyPropertyChanged
    {
        readonly IUnityContainer _container;
        readonly StorageService _storage;

        WeighDataRepository _repository;

        public FiltersManagerPresenter FilterManager { get; protected set; }
        public PageInfoPresenter Page { get; private set; }

        public ICommand ShowCameraNavigatorCommand { get; protected set; }
        public ICommand NextPageCommand { get; protected set; }
        public ICommand PrevPageCommand { get; protected set; }
        public ICommand RefreshCommand { get; protected set; }

        public WeighDataPresenter(IUnityContainer container, StorageService storage, FiltersManagerPresenter filterManager)
        {
            Page = new PageInfoPresenter();
            _container = container;
            _storage = storage;
            FilterManager = filterManager;

            ShowCameraNavigatorCommand = new DelegateCommand<Weigh>(ShowCameraNavigator);
            RefreshCommand = new DelegateCommand<object>(Refresh);
            NextPageCommand = new DelegateCommand<object>(NextPage);
            PrevPageCommand = new DelegateCommand<object>(PrevPage);
        }

        void ShowCameraNavigator(Weigh obj)
        {
            _container.Resolve<ModalViewManager>().Show(_container.Resolve<Views.WeighDataCamerasView>());
            Application.Current.MainWindow.UpdateLayout();
            _container.Resolve<IEventAggregator>().GetEvent<ShowMovieForWeightDataEvent>().Publish(obj);
          
        }
        void Refresh(object obj = null)
        {
            OnPropertyChanged("Objects");
        }
        void NextPage(object obj = null)
        {
            Page.Index++;
            Refresh(obj);
        }
        void PrevPage(object obj = null)
        {
            Page.Index--;
            Refresh(obj);
        }
        
        IEnumerable<IStorageFilter> CurrentFilters
        {
            get
            {
                if (FilterManager.WasChanged)
			    {
				    Page.Index = 0;
			    }
                var additionalFilters = FilterManager.RecreateActiveFilters();
                var filters = new List<IStorageFilter>(additionalFilters);
                filters.Add(new PagingFilter(PageInfoPresenter.CAPACITY, Page.Index));
                filters.Add(new SortFilter("WeighTime" , SortFilter.Orders.DESC));
                return filters;
            }
        }

        public IList<Weigh> Objects
        {
            get
            {
                if (_repository == null) _repository = _storage.CreateWeighDataRepository();

                IList<Weigh> result = new Weigh[0];
                if (!_storage.SafeDo(() => result = _repository.GetAll(CurrentFilters) ?? result))
                {
                    _repository.CloseSession();
                    _repository = null;
                }
                else
                {
                    Page.setPropertyForCountObjects(result.Count);
                }
                return result;
            }
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

