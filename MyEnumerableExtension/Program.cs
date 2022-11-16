namespace MyEnumerableExtension
{

    public static class Extensions
    {

        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> projection)
        {
            foreach (var item in source)
            {
                yield return projection(item);
            }
        }
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
        public static TSource MyFirst<TSource>(this IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();
            var canMove = enumerator.MoveNext();
            if (!canMove)
                throw new Exception();

            return enumerator.Current;
        }

        public static TSource MyFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new Exception();

            var firstValidItem = default(TSource);
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    firstValidItem = item;
                    break;
                }
            }
            return firstValidItem;
        }

        public static bool MyAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool allMatched = true;
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    allMatched = false;
                    break;
                }
            }
            return allMatched;
        }

        public static bool MyAny<TSource>(this IEnumerable<TSource> source)
        {
            bool any = false;
            foreach (var item in source)
            {
                any = true;
                break;
            }
            return any;
        }

        public static bool MyAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool any = false;
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    any = true;
                    break;
                }
            }
            return any;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var myFirst = list.MyFirst();
            var myFirstConditioned = list.MyFirst(o => o != 1);
            var myAll = list.MyAll(n => n < 11);
            Console.WriteLine(myFirst);
            Console.WriteLine(myFirstConditioned);
            Console.WriteLine(myAll);
        }
    }
}