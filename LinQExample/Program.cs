namespace LinQExample
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //aggregate = fold
            //1, 2, 3, 4, 5, 6, 7
            // --> x
            // 1 -> 2 -> 3 -> 4.. -> x
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sum = 0;
            foreach (var item in list)
                sum += item;

            //foreach alternatives
            //seed = start
            //a = old value
            //i current value
            var sum2 = list.Aggregate(0, (a, i) => a + i);
            Console.WriteLine(sum2);
            var sum3 = list.Sum();

            var sum4 = list.MyAggregate(0, (a, i) => a + i);
            Console.WriteLine(sum4);

            //var max = list.MyAggregate(0, (a, i) =>
            //{
            //    var max = -1;
            //    if (i > a)
            //        max = i;
            //    return max;
            //});
            var max = list.MyAggregate(0, (a, i) => i > a ? i : a);
            Console.WriteLine(max);

            var list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            var list2 = new List<int> { 5, 6, 7, 8, 9, 10, 11 };

            var combined = list1.MyConcat(list2);
            foreach (var item in combined)
                Console.Write("{0} ", item);

            Console.WriteLine("-----");
            var unioned = list1.MyUnion(list2, EqualityComparer<int>.Default);
            foreach (var item in unioned)
                Console.Write("{0} ", item);

            var listA = new List<string> { "Hello", "world" };
            var listB = new List<string> { "HELLO", "whoa", "STUFF", "Stuff" };

            foreach (var item in listA.MyUnion(listB, StringComparer.CurrentCultureIgnoreCase))
                Console.WriteLine("{0} ", item);
        }
    }
    public static class MyExtension
    {
        public static TAcc MyAggregate<TAcc, TSource>(this IEnumerable<TSource> that, TAcc seed, Func<TAcc, TSource, TAcc> fold)
        {
            var value = seed;
            foreach (var item in that)
            {
                value = fold(value, item);
            }

            return value;
        }

        public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
        {
            //IMPORTANT YOU CANNOT CREATE LIST HERE AND ADD THEM ONE BY ONE.
            //IN CASE OF INFINITE SEQUNCE LIKE EVENNUMBERS THIS CODE WOULD HANG.

            //debug this to see how it works.
            //tldr: it reads the first array then yields second.
            foreach (var item in that)
                yield return item;

            foreach (var item in rhs)
                yield return item;
        }
        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> that, IEnumerable<T> rhs, IEqualityComparer<T> comparer)
        {
            var set = new HashSet<T>(comparer);
            foreach (var item in that)
                if (set.Add(item))
                    yield return item;

            foreach (var item in rhs)
                if (set.Add(item))
                    yield return item;
        }

        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
        {
            return MyUnion(that, rhs, EqualityComparer<T>.Default);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
        {
            var blacklist = new HashSet<T>(rhs);
            foreach (var item in that)
                if (blacklist.Add(item))
                    yield return item;
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> that, IEnumerable<T> rhs)
        {
            var itemsThatExist = new HashSet<T>(rhs);
            foreach (var item in that)
                if (itemsThatExist.Remove(item))
                    yield return item;

        }

        public static Dictionary<TKey,TValue> MyToDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keyProjection,
            Func<TSource, TValue> valueProjection)
        {
            var dict = new Dictionary<TKey, TValue>();
            foreach(var item in source)
            {
                var key = keyProjection(item);
                var val = valueProjection(item);
                dict.Add(key, val);
            }
            return dict;

        }

        static IEnumerable<int> EvenNumbers()
        {
            for (var i = 0; ; i += 2)
                if ((i % 2) == 0)
                    yield return i;
        }
    }


}