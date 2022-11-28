namespace ImplicitTyping
{
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
            var peopleDB = new[]
            {
                new Person(0,"Rainbow", "Dash"),
                new Person(1,"Rarity", "?"),
                new Person(2,"Calamity", "Dashite"),
                new Person(3,"Colgate", "?"),
                new Person(4,"Nelson", "LaQuet"),
                new Person(5, "red", "fye")
            };

            var person = new Person(0, "Rainbow", "Dash");

            //Compiler generates Unspeakable type. Has to have var.
            var anonymousType = new {
                Prop1 = 10,
                Prop2 = "hey",
                //Dont do this FirstName = person.Fistname.
                person.FirstName,
                //wont work, lamdas do not have type, at compile time its transformed to delegate
                //Whoa = () => Console.WriteLine("Hey")

                Whoa = (Action)(() => Console.WriteLine("Hey"))

            };
            //anonymousType.Prop1 = 25; read only.
            Console.WriteLine(anonymousType.Prop1);
            Console.WriteLine(anonymousType.Whoa);

            var hey = Identity(new { person.FirstName });

            //anonymous types were built for linq.
            var grouped = peopleDB.GroupBy(k => k.FirstName[0], (t, people) => new
            {
                FirstLetter = t,
                Count = people.Count(),
                AverageNameLength = people.Select(p => p.FirstName.Length).Average()
            });

            foreach(var group in grouped.Where(g => g.AverageNameLength > 7))
            {
                Console.WriteLine("For people with the first letter of {0}, there are {1} and average at {2}", group.FirstLetter, group.Count, group.AverageNameLength);
            }

        }

        static T Identity<T>(T value)
        {
            return value;
        }
    }
}