using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPF.Patterns.ViewModel;

namespace WPF.Patterns.Collections
{
    public static class ObservableCollectionHelper
    {
        public static ObservableCollection<TOutput> ConvertAll<T, TOutput>(IEnumerable<T> src, Converter<T, TOutput> converter)
        {
            ObservableCollection<TOutput> output = new ObservableCollection<TOutput>();
            ConvertAll(output, src, converter);
            return output;
        }

        public static void ConvertAll<T, TOutput>(ObservableCollection<TOutput> output, IEnumerable<T> src, Converter<T, TOutput> converter)
        {
            if (src == null) return;
            foreach (T element in src)
            {
                TOutput outputItem = converter(element);
                output.Add(outputItem);
            }
        }
        
        public static void ClearAll<TOutput>(ObservableCollection<TOutput> output)
        {
            if (output.Count > 0) output.Clear();
        }

        public static void UpdateOrConvertAll<T, TOutput>(ObservableCollection<TOutput> output, IEnumerable<T> src, Converter<T, TOutput> converter)            
        {
            if (src == null)
            {
                ClearAll(output);                
                return;
            }

            ClearAll(output);             
            foreach (T element in src)
            {                
                output.Add(converter(element));                
            }
        }
    }
}
