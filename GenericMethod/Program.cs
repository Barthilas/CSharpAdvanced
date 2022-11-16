namespace GenericMethod
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
    class Whoa
    {

        //Comment out for error, because T is specified only for default constructor.
        public Whoa() { }
        public Whoa(string name) { }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Example 1
            var testClass = Hey<Whoa>();

            //Example 2
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            var odd = Filter<int>(list, i => i % 2 != 0);
            var even = Filter(list, i => i % 2 == 0);

            Console.WriteLine("ODD: ");
            foreach (var item in odd)
                Console.WriteLine(item);

            Console.WriteLine("EVEN: ");
            foreach (var item in even)
                Console.WriteLine(item);

            //Example 3
            var people = new List<Person> { new Person("Aaron", "p2"), new Person("Andrew", "p2"), new Person("Nelson", "p2"), };
            var peopleStartingWIthA = new List<Person>();
            foreach (var person in people)
            {
                if (person.FirstName.Length > 0 && person.FirstName.ToLower()[0] == 'a')
                {
                    peopleStartingWIthA.Add(person);
                }
            }

            // Example 3 better
            // IEnumerable is not a list! Only provides a next value till end.
            IEnumerable<Person> people2 = Filter<Person>(people, p => p.FirstName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase));
            people.Add(new Person("ANoticeWhenAdded1", "person"));

            Console.WriteLine("FIRST ITERATION: ");
            foreach (var item in people2)
                Console.WriteLine(item.FirstName);

            people.Add(new Person("ANoticeWhenAdded2", "person"));

            Console.WriteLine("SECOND ITERATION: ");
            foreach (var item in people2)
                Console.WriteLine(item.FirstName);

            // Example 4 foreach -> map -> filter. This is the foundation on LINQ.
            IEnumerable<string> firstNames = Map<Person, string>(people2, p => p.FirstName);
            var fistNames2 = Map(people, p => p.FirstName);

            foreach(var name in fistNames2)
            {
                Console.WriteLine(name);
            }
        }

        // T must contain parametless constructor.
        static T Hey<T>() where T : new()
        {
            //without where throws error.
            return new T();
        }

        // List<int> -> IEnumerable<T>
        // IEnumerable<int> -> IEnumerable<T>
        // T = int
        static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        static IEnumerable<TResult> Map<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> projection)
        {
            foreach (var item in source)
            {
                yield return projection(item);
            }
        }
    }
}