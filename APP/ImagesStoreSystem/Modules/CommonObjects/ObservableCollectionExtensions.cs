using System;
using System.Collections.ObjectModel;

namespace CommonObjects
{
	public static class ObservableCollectionExtensions
	{
		public static void Sort<T>(this ObservableCollection<T> collection, Func<T, T, int> comparer)
		{
			bool isSorted = false;
			int count = collection.Count;
			while (!isSorted)
			{
				isSorted = true;

				for (int i = 1; i < count; ++i)
				{
					if (comparer(collection[i - 1], collection[i]) > 0)
					{
						isSorted = false;
						T store = collection[i];
						collection[i] = collection[i - 1];
						collection[i - 1] = store;
					}
				}
			}
		}
	}
}
