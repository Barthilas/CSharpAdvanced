namespace Hoisting
{
    delegate bool IntPredicate(int number);
    internal class Program
    {
        // Hoisted representation.
        //class DisplayClass1
        //{
        //    public int number;
        //    public bool Method(int n)
        //    {
        //        number++;
        //        return (n % 5 == 0) && (n % 3 == 0);
        //    }
        //}

        static void Main(string[] args)
        {
            //#region COMPILER LIKE HOISTING EXAMPLE DEMO
            //var displayClass = new DisplayClass1();
            //displayClass.number = 10;
            //Console.WriteLine(displayClass.number);
            //#endregion

            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
            var number = 10;
            var filteredList = Filter(arr, n =>
            {
                //hoisted
                number++;
                return (n % 5 == 0) && (n % 3 == 0);
            });

            // result is 27.
            Console.WriteLine(number);
        }

        static List<int> Filter(IEnumerable<int> source, IntPredicate predicate)
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