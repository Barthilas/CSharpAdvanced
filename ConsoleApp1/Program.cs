// () =>            NO PARAMS
// x =>             ONE PARAM
// (x) =>           ONE PARAM
// (x,y,z) =>       MULTIPLE PARAMS

// SINGLE EXPRESSION LAMBDA (imlicit return if converted to a deleagate that return a value)
// () =>            expression

// MULTI EXPRESSION LAMBDA (no implicit return, return must be written out)
// () => {statemen1; statement2;}

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
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            //Test 1
            //IntPredicate predicate = IsMod3;
            //predicate += IsMod5; //only this will be returned.
            //var filteredList = Filter(arr, predicate);

            //Test 2
            //var filteredList = Filter(arr, IsMod3);

            //Test 3
            //Anonymous (not named) delegate - outdated. Why? To not create tiny methods isMod...
            //var filteredList2 = Filter(arr, delegate (int number)
            //{
            //    return (number % 5 == 0);
            //});

            //Test4
            //Lambda syntax C# 3+
            //var filteredList = Filter(arr, (int number) =>
            //{
            //    return (number % 5 == 0);
            //});
            //or
            //var error = n => n % 5 == 0;
            //IntPredicate notError = n => n % 5 == 0;
            //var filteredList = Filter(arr, n => n % 5 == 0);
            //filteredList = Filter(filteredList, n => n % 3 == 0);

            //var filteredList = Filter(arr, n => (n % 5 == 0) && (n % 3 == 0));

            var filteredList = Filter(arr, n =>
            {
                return (n % 5 == 0) && (n % 3 == 0);
            }); 

            foreach (var item in filteredList)
            {
                Console.WriteLine(item);
            }
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