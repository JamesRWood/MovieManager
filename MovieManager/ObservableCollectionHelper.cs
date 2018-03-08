namespace MovieManager
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class ObservableCollectionHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in list)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ObservableCollection<T> collection)
        {
            var list = new List<T>();
            foreach (var item in collection)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
