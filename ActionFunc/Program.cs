namespace ConsoleApp1
{
    internal class Program
    {

        //These are generic. Basically they made delegates obsolete.
        //Predefined generic delegates
        // Action = delegate = void ()
        // Action<int> = void (int)
        // Action<int, float> = void(int, float)
        // Action<int, float, string> = void(int, float, string)

        // Func<int> = int()
        // Func<float, int> = int(float)
        // Func<string, float, int> = int(string, float)

        static void Main(string[] args)
        {
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            var filteredList = Filter(arr, n => (n % 5 == 0) && (n % 3 == 0));

            foreach (var item in filteredList)
            {
                Console.WriteLine(item);
            }
        }

        static List<int> Filter(IEnumerable<int> source, Func<int, bool> predicate)
        {
            var list = new List<int>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}