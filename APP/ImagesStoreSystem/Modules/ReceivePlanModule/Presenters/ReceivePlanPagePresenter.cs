using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;
using CommonObjects;
using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;
using StorageModule.Services;

namespace ReceivePlanModule.Presenters
{
	public class ReceivePlanPagePresenter : PagePresenter<ReceivePlanTask>
	{
		static SortFilter PlanTimeSortFilter = new SortFilter("StartTime");

		ReceivePlanTaskRepository _repository;
		ReceivePlanTaskRepository _repoForSave;

		

		public FiltersManagerPresenter<IReceivePlanTaskFilter> FilterManager { get; protected set; }

		public ICommand ImportCommand { get; protected set; }
		public ICommand ExportCommand { get; protected set; }
		public ICommand ShowAttachetSessionCommand { get; protected set; }
		public ICommand AttachResultCommand { get; protected set; }

		public ReceivePlanPagePresenter(StorageService storage, IEventAggregator eventAggregator, FiltersManagerPresenter<IReceivePlanTaskFilter> filterManager, int pageCapacity = 20)
			: base(eventAggregator, pageCapacity)
		{
			if (storage == null) throw new ArgumentNullException("storage");
			if (filterManager == null) throw new ArgumentNullException("filterManager");

			FilterManager = filterManager;
			_repository = storage.CreateReceivePlanTaskRepository();
			_repoForSave = storage.CreateReceivePlanTaskRepository();

			_eventAggregator.GetEvent<RefreshPagesEvent>().Subscribe(
				obj =>
				{
					PageNumber = 0;
					Refresh();
				}, true);
			_eventAggregator.GetEvent<RefreshAllEvent>().Subscribe(arg => Refresh(), ThreadOption.UIThread, true);

			InitCommands();
			
		}

		void InitCommands()
		{
			ImportCommand = new DelegateCommand<object>(
				arg =>
				{
					OpenFileDialog dlg = new OpenFileDialog();
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						RecivePlanFile parser = new RecivePlanFile();
						int plansCount = 0;
						int savedPlans = 0;
						using (var fileStream = dlg.OpenFile())
						{
							var plans = parser.Parse(fileStream);
							if (plans == null)
							{
								//TODO сделать можальное окно
								MessageBox.Show(string.Format("Неверный формат файла {0}", dlg.FileName));
							}
							else
							{
								foreach (var plan in plans)
								{
									plansCount++;
									try
									{
										_repository.Save(plan);
										savedPlans++;
									}
									catch (OperationException)
									{
									}
								}
							}
						}
						if (plansCount > savedPlans)
						{
							//TODO сделать можальное окно
							MessageBox.Show(string.Format("Импортировалось {0} заданий из {1}", savedPlans, plansCount));
						}
						_eventAggregator.GetEvent<RefreshAllEvent>().Publish(null);
					}
				});
			ExportCommand = new DelegateCommand<object>(
				arg =>
				{
					SaveFileDialog dlg = new SaveFileDialog();
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						using (var fileStream = dlg.OpenFile())
						{
							var filters = CreateFilters(false);
							RecivePlanFile parser = new RecivePlanFile();
							parser.Write(_repoForSave.GetAll(filters), fileStream);
						}
					}

				});
			ShowAttachetSessionCommand = new DelegateCommand<object>(
				obj =>
				{
					var task = obj as ReceivePlanTask;
					if (task != null && task.ResultSession != null)
					{
						_eventAggregator.GetEvent<ViewDetailsOfRSEvent>().Publish(new EditEventArgs(task.ResultSession));
					}
				});
			AttachResultCommand = new DelegateCommand<object>(
				arg => 
				{
					var task = arg as ReceivePlanTask;
					if (task != null)
					{
						_eventAggregator.GetEvent<EditReceiveSessionEvent>().Publish(new EditEventArgs(
							new ReceiveSession(task.StationCatalogNumber, task.SatelliteCatalogNumber)
							{
								StartTime = task.StartTime,
								EndTime = task.EndTime
							}, null,
							obj =>
							{
								var session = obj as ReceiveSession;
								if (session != null)
								{
									task.ResultSession = session;
									_repository.SaveAllChanges();
									Refresh();
								}
							}));
					}
				});
		}

		public override IList<ReceivePlanTask> Objects
		{
			get
			{
				List<IReceivePlanTaskFilter> filters = CreateFilters();
				IList<ReceivePlanTask> res = null;
				ActionWithConnectException(() => res = _repository.GetAll(filters) ?? new ReceivePlanTask[0]);

				return RefreshWarningState(res);
			}
		}

		protected override IList<ReceivePlanTask> RefreshWarningState(IList<ReceivePlanTask> plansOnPage)
		{
			if (plansOnPage.Count == 0)
			{
				if (FilterManager.Filters.Count > 0)
				{
					Warning = PageWarning.NoObjectsForThisFilter;
				}
				else
				{
					Warning = PageWarning.NoObjectsInDB;
				}
			}
			return base.RefreshWarningState(plansOnPage);
		}

		List<IReceivePlanTaskFilter> CreateFilters(bool addPageFilter = true)
		{
			List<IReceivePlanTaskFilter> filters = new List<IReceivePlanTaskFilter>();
			if (FilterManager.WasChanged)
			{
				_pageNumber = 0;
				OnPropertyChanged("PageNumber");
			}
			var additionalFilters = FilterManager.RecreateActiveFilters();
			filters = new List<IReceivePlanTaskFilter>(additionalFilters);
			if (addPageFilter)
			{
				filters.Add(new PagingFilter(PageCapacity, PageNumber));
			}
			filters.Add(PlanTimeSortFilter);
			return filters;
		}
	}
}
