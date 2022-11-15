namespace ConsoleApp1
{
    delegate bool IntPredicate(int number);
    internal class Program
    {
        static bool IsMod3(int number)
        {
            return number % 3 == 0;
        }

        static bool IsMod5(int number)
        {
            return number % 5 == 0;
        }

        static void Main(string[] args)
        {
            var arr = new[] {1,2,3,4,5,6};

            //IntPredicate predicate = IsMod3;
            //predicate += IsMod5; //only this will be returned.

            var filteredList = Filter(arr, IsMod3);

            foreach (var item in filteredList)
            {
                Console.WriteLine(item);
            }
        }

        static List<int> Filter(IEnumerable<int> source, IntPredicate predicate)
        {
            var list = new List<int>();
            foreach(var item in source)
            {
                if(predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}