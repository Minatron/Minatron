using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPF.Patterns.Collections;
using System.Windows.Threading;

namespace WPF.Patterns.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged ,IDisposable
    {                

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            this.OnDispose();
        }
        protected virtual void OnDispose()
        {
        }          
        #endregion

        protected static Dispatcher Dispatcher;
        static ViewModelBase()
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
        }

        public static void SetViewModel(ViewModelBase vm, FrameworkElement view)
        {            
            view.DataContext = vm;           
        }

        public static Window GetWindow(FrameworkElement view)
        {
            return GetWindow(view, "");
        }
        public static Window GetWindow(FrameworkElement view,string Title)
        {
            var window = new Window();
            window.Content = view;
            window.Title = Title;
            return window;
        }

        public static void ShowViewModel(ViewModelBase vm, Window view)
        {
            SetViewModel(vm, view);
            view.Show();
        }


        public static VM ClearOrCreate<VM>(VM viewModel)
            where VM : IUpdatebleViewModel, new()            
        {
            if (viewModel == null) viewModel = new VM();
            else viewModel.ClearModel();
            return viewModel;
        }
        public static VM UpdateOrCreate<VM, T>(VM viewModel, T model)
            where VM : UpdatableViewModelBase<T>, new()
            where T : class
        {
            if (viewModel == null) viewModel = new VM();                        
            viewModel.Model=model;            
            return viewModel;
        }

        public static ObservableCollection<TOutput> ClearOrCreate<TOutput>(ObservableCollection<TOutput> output)
        {
            if (output == null) output = new ObservableCollection<TOutput>();
            else if (output.Count >0) output.Clear();            
            return output;
        }

        public static ObservableCollection<TOutput> UpdateOrCreate<T, TOutput>(ObservableCollection<TOutput> output, IEnumerable<T> src, Converter<T, TOutput> converter)
        {
            if (output == null) output = new ObservableCollection<TOutput>();
            ObservableCollectionHelper.UpdateOrConvertAll(output, src, converter);
            return output;
            
        }



    }
}
