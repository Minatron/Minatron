using System;
using System.Collections.Generic;
using System.ComponentModel;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;
using Settings.Infrastucture;

namespace SettingsModule.Presenters
{
    public class LangSettingPresenter : INotifyPropertyChanged
    {
		Properties.Settings _settings;

        string m_lang;

        public string CurrentLang 
        {
            get
            {
                return m_lang;
            }
            set
            {
                m_lang = value;
                OnPropertyChanged("CurrentLang");
            }
        }

        public IList<String> Langs
        {
            get
            {
                return Lang.GetLangs();
            }
        }

        public LangSettingPresenter(Properties.Settings settings, IEventAggregator eventAggregator)
        {
			_settings = settings;

			eventAggregator.GetEvent<SettingsSaveEvent>().Subscribe(Save, true);
            eventAggregator.GetEvent<SettingsReloadEvent>().Subscribe(Reset, true);

			Reset();
        }

		void Save(object o)
		{
			_settings.Lang = CurrentLang;
			_settings.Save();
			Lang.SetLang(_settings.Lang);
		}
		void Reset(object o = null)
		{
			CurrentLang = _settings.Lang;
		}

        #region INotifyPropertyChanged
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
