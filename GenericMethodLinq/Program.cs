using System.Linq;

namespace GenericMethodLinq
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person> { new Person("Aaron", "p2"), new Person("Andrew", "p2"), new Person("Nelson", "p2"), };
            //var firstNames = people.Filter(i => i.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase))
            //    .Map(p => p.FirstName);

            // LINQ way
            var firstNames = people.Where(i => i.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase))
                .Select(p => p.FirstName);

            foreach (var name in firstNames)
                Console.WriteLine(name);

            var firstName = people.FirstOrDefault(i => i.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase));

            //Also LINQ but in query expression.
            var firstNamesQuery = from p in people where p.FirstName.StartsWith("a") orderby p.LastName select p.FirstName;

            var numbers = Enumerable.Range(0, 100).Select(i => i * i).Where(i => i < 50);
            // faster than creating list
            var numbers1 = Enumerable.Empty<int>();
            // create array of 20 strings.
            var repeat = Enumerable.Repeat("a", 20);

            //Immutable. You cannot add, delete.
            var concat = new[] { 1, 2, 3, 4 }.Concat(new[] { 5, 6, 7, 8, 9 });

            foreach (var item in concat)
            {
                //begin user code
                Console.WriteLine(item);
                //end user code
            }


            //foreach EQUALS TO this

            {
                var enumerator = concat.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var item = enumerator.Current;
                        //begin user code
                        Console.WriteLine(item);
                        //end user code
                    }

                }
                finally
                {
                    enumerator.Dispose();
                }
            }

            IQueryable<int> blegh = new[] { 1, 2, 3 }.AsQueryable();
            blegh.Where(i => i > 5);
        }
    }

    public static class EnumerableExtensions
    {
        //LINQ: Where
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        //LINQ: Select
        public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> projection)
        {
            foreach (var item in source)
            {
                yield return projection(item);
            }
        }
    }
}