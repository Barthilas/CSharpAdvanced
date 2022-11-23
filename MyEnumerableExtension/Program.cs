namespace MyEnumerableExtension
{

    public static class Extensions
    {
        public static int MyCount<T>(this IEnumerable<T> that)
        {
            //optimization
            //down-cast, doesnt throw exception vs (ICollection)that.
            var collection = that as ICollection<T>;
            if (collection != null)
            {
                Console.WriteLine("FAST METHOD");
                return collection.Count;
            }

            Console.WriteLine("SLOW METHOD");
            //will never end for unlimited sequence.
            var count = 0;
            foreach (var item in that)
                count++;

            return count++;
        }

        public static int MyCount<T>(this IEnumerable<T> that, Func<T, bool> predicate)
        {
            var count = 0;
            foreach (var item in that)
                if(predicate(item))
                    count++;

            return count++;
        }
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

            //using (var enumerator = source.GetEnumerator())
            //{
            //    if (!enumerator.MoveNext())
            //        throw new Exception();

            //    return enumerator.Current;
            //}


            foreach (var item in source)
            {
                return item;
            }

            throw new InvalidOperationException();
        }

        public static TSource MyFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
                if (predicate(item))
                    return item;

            throw new InvalidOperationException();
        }

        public static bool MyAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool MyAny<TSource>(this IEnumerable<TSource> source)
        {
            foreach (var item in source)
            {
                return true;
            }
            return false;
        }

        public static bool MyAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var myFirst = list.MyFirst();
            //var myFirstConditioned = list.MyFirst(o => o != 1);
            //var myAll = list.MyAll(n => n < 11);
            //Console.WriteLine(myFirst);
            //Console.WriteLine(myFirstConditioned);
            //Console.WriteLine(myAll);

            //Important.
            list.MyCount();

            var projection = list.MySelect(i => i * i);
            projection.MyCount();
        }
    }

    //Better encapsulation
    class Inventory
    {
        private readonly List<Product> _products;
        public IEnumerable<Product> Products { get { return _products; } }

        public Inventory()
        {
            _products = new List<Product>();
        }

    }

    internal class Product
    {
    }
}