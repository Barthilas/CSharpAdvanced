namespace IteratorBlock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var items = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (var blegh in MyIterratorBlock(FilterMin(items, 6)))
            {
                Console.WriteLine(blegh);
            }
        }

        static IEnumerable<int> FilterMin(IEnumerable<int> source, int min)
        {
            foreach (int i in source)
            {
                if (i > min)
                {
                    yield return i;
                }
            }
        }

        static IEnumerable<int> MyIterratorBlock(IEnumerable<int> souce)
        {
            foreach (int i in souce)
                yield return i * i;
        }
    }
}