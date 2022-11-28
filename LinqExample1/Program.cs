using System.Collections;

namespace LinqExample1
{
    class Address
    {
        public int PersonId { get; set; }
        public string AddressString { get; set; }

        public Address(int personId, string address)
        {
            this.PersonId = personId;
            AddressString = address;
        }
    }
    class Person
    {
        public Person(int id, string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            this.Id = id;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var objs = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var floats = objs.MyCast<float>();

            var people = new[]
            {
                new Person(0,"Rainbow", "Dash"),
                new Person(1,"Rarity", "?"),
                new Person(2,"Calamity", "Dashite"),
                new Person(3,"Colgate", "?"),
                new Person(4,"Nelson", "LaQuet"),
                new Person(5, "red", "fye")
            };

            var addressDb = new[]
            {
                new Address(0, "Cloudsdale"),
                new Address(1, "Prague street"),
                new Address(2, "Stuff"),
                new Address(3, "Phoenix Avenue"),
            };

            Console.WriteLine("---- ToLookup");

            //This is not deffered as ILookup has count, its collection.
            //behaves like index instead of dictionary
            ILookup<char, Person> lookup = people.ToLookup(k => k.FirstName.Length > 0 ? k.FirstName[0] : ' ');
            foreach (var i in lookup)
            {
                Console.WriteLine("Key: {0}", i.Key);

                foreach (var person in i)
                {
                    Console.WriteLine("--- {0} {1}", person.FirstName, person.LastName);
                }
            }
            Console.WriteLine("----- lookup test");
            var firstNamesWithR = lookup['R'];

            foreach (var person in firstNamesWithR)
            {
                Console.WriteLine("{0}", person.FirstName);
            }

            //Lookup vs dict error as key exists
            //var dict = people.ToDictionary(k => k.FirstName.Length > 0 ? k.FirstName[0] : ' ');
            //foreach(var kv in dict)
            //{
            //    Console.WriteLine("{0} - {1}", kv.Key, kv.Value.FirstName);
            //}

            Console.WriteLine("---- Myjoin");
            var addresses = people.MyJoin(addressDb,
                o => o.Id,
                i => i.PersonId,
                (p, a) => string.Format("An address for {0} is {1}", p.FirstName, a.AddressString),
                EqualityComparer<int>.Default
                );

            foreach (var address in addresses)
            {
                Console.WriteLine(address);
            }

            Console.WriteLine("---- GroupBy -- similar to lookup");
            //vs lookup: this method is deffered. Its IEnumerable, not collection.
            //keyselector
            //elementselector
            //resultselector
            //var group = people.GroupBy(k => k.FirstName.EndsWith("y", StringComparison.CurrentCultureIgnoreCase));
            var group = people.GroupBy(k => k.FirstName.Length != 0 ? k.FirstName[k.FirstName.Length - 1] : ' ',
                v => v.FirstName);

            var group2 = people.GroupBy(k => k.FirstName.Length != 0 ? k.FirstName[k.FirstName.Length - 1] : ' ',
                (k, r) => string.Format(r.Count() == 1 ? "There is {0} person whos first name ends with {1}" : "There are {0} people whos first name ends with {1}",
                r.Count(), k));
            foreach (var item in group)
            {
                Console.WriteLine(item.Key);
                foreach (var person in item)
                {
                    //Console.WriteLine("-- {0} {1}", person.FirstName, person.LastName);
                    Console.WriteLine("-- {0}", person);
                }
            }

            Console.WriteLine("---- GroupBy2 ----");
            foreach (var ppl in group2)
            {
                Console.WriteLine(ppl);
            }


            //Zip two unrelated data sources
            var notes = new[] { "Note 1", "Note 2", "Note 3", "Note 4" };
            var zipped = people.Zip(notes, (p, n) => string.Format("Person named {0} has these notes: {1}", p.FirstName, n));

            foreach (var note in zipped)
            {
                Console.WriteLine(note);
            }

            // different syntax
            var ppl22 = from person in people where
                      person.FirstName.StartsWith("R") orderby person.LastName select person.LastName;

            var ppl23 = people.Where(p => p.FirstName.StartsWith("R")).OrderBy(p => p.LastName).Select(p => p.LastName);
        }
    }

    public static class MyExtensions
    {
        //reduce parameters
        //public static IEnumerable<TResult> MyCast<TSource, TResult>(this IEnumerable<TSource> that)

        public static IEnumerable<TResult> MyCast<TResult>(this IEnumerable that)
        {
            foreach (var item in that)
                yield return (TResult)item;


        }

        public static IEnumerable<TResult> MyOfType<TResult>(this IEnumerable that)
        {
            foreach (var item in that)
            {
                //doesn't work.
                //var attemptedCast = item as TResult;
                //if(attemptedCast != null)
                //    yield return attemptedCast;

                if (item is TResult)
                    yield return (TResult)item;
            }
        }

        public static IEnumerable<TResult> MyJoin<TResult, TOuter, TInner, TKey>(
            this IEnumerable<TOuter> that,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> projection,
            IEqualityComparer<TKey> comparer)
        {
            foreach (var outerElement in that)
            {
                var outerKey = outerKeySelector(outerElement);
                foreach (var innerElement in inner)
                {
                    var innerKey = innerKeySelector(innerElement);
                    if (comparer.Equals(outerKey, innerKey))
                        yield return projection(outerElement, innerElement);
                }

            }
        }

        public static IEnumerable<TResult> MyJoin<TResult, TOuter, TInner, TKey>(
           this IEnumerable<TOuter> that,
           IEnumerable<TInner> inner,
           Func<TOuter, TKey> outerKeySelector,
           Func<TInner, TKey> innerKeySelector,
           Func<TOuter, TInner, TResult> projection)
        {
            return MyJoin(that, inner, outerKeySelector, innerKeySelector, projection, EqualityComparer<TKey>.Default);
        }

        public static bool MySequenceEqual<TSource>(
            this IEnumerable<TSource> that,
            IEnumerable<TSource> second)
        {
            return MySequenceEqual(that, second, EqualityComparer<TSource>.Default);
        }

        public static bool MySequenceEqual<TSource>(
            this IEnumerable<TSource> that,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            using (var thatEnumerator = that.GetEnumerator())
            {
                using (var secondEnumerator = second.GetEnumerator())
                {
                    while (true)
                    {
                        var thatHasMoreItems = thatEnumerator.MoveNext();
                        var secondHasMoreItems = secondEnumerator.MoveNext();
                        if (!thatHasMoreItems && !secondHasMoreItems)
                            return true;

                        if (thatHasMoreItems != secondHasMoreItems)
                            return false;

                        // cannot use != (reference comparison)
                        if (!comparer.Equals(thatEnumerator.Current, secondEnumerator.Current))
                            return false;
                    }
                }
            }

        }

        public static IEnumerable<TResult> Zip<TSource, TSecond, TResult>(
            this IEnumerable<TSource> that,
            IEnumerable<TSecond> second,
            Func<TSource, TSecond, TResult> projection)
        {
            using (var thatEnumerator = that.GetEnumerator())
            using (var secondEnumerator = second.GetEnumerator())
                while (thatEnumerator.MoveNext() && secondEnumerator.MoveNext())
                    yield return projection(thatEnumerator.Current, secondEnumerator.Current);
        }
    }
}