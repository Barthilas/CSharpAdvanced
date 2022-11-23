namespace IEnumerableConfussion
{
    interface IMyEnumerator<T>
    {
        T Current { get; }
        bool MoveNext();
    }

    interface IMyEnumerable<T>
    {
        IMyEnumerator<T> GetEnumerator { get; }
    }

    class MyList<T> : IMyEnumerable<T>
    {
        public IMyEnumerator<T> GetEnumerator => throw new NotImplementedException();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6};
            IEnumerable<int> filtered = numbers.Where(i => i > 4);
            IEnumerable<int> squared = filtered.Select(i => i * i);
            //See how it changes. Doesnt reflect the add anymore.
            //IEnumerable<int> squared = filtered.Select(i => i * i).ToList();

            foreach (var item in squared)
                Console.WriteLine(item);

            numbers.Add(10);

            Console.WriteLine("-----------------");

            foreach (var item in squared)
                Console.WriteLine(item);
        }
    }
}