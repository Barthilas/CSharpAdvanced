namespace CSharpAdvanced
{
    //represents method as object. It's type, cannot override.
    delegate void FizzBuzzOutput(string output);
    class Blegh
    {
        private readonly string _prefix;
        public Blegh(string prefix)
        {
            _prefix = prefix;
        }

        public void DoStuff(string output)
        {
            Console.WriteLine("FROM BLEGH {0} - {1}", _prefix, output);
        }

        public override string ToString()
        {
            return "Blegh with prefix of " + _prefix;
        }
    }
    class Program
    {
        static void WriteFizzBuzz(string output)
        {
            Console.WriteLine(output);
        }

        static void WriteFizzBuzz(int number)
        {
           
        }

        static void Main(string[] args)
        {
            //method group, Intellisense highlights only the compatible.
            //Run(WriteFizzBuzz, 1, 100);

            //Run(input => Console.WriteLine(input), 1, 100);

            //Won't work.
            //Run(Console.WriteLine(), 1, 100);

            //Run(Console.WriteLine, 1, 100);

            var blegh1 = new Blegh("Lil'pip");
            var blegh2 = new Blegh("Calamity");
            //Run(blegh1.DoStuff, 1, 3);
            //Run(blegh2.DoStuff, 1, 3);

            //Invocation lists - less code (appending delegate)
            //order matters.
            FizzBuzzOutput myOutput = blegh1.DoStuff;
            myOutput += blegh2.DoStuff;
            myOutput += WriteFizzBuzz;

            Run(myOutput, 1, 3);
        }
        public static void Run(FizzBuzzOutput output, int from, int count)
        {
            //Delegate has reference to instance and method. (target.method(params))
            //target will always be null for static method
            //Console.WriteLine(output.Method);
            //Console.WriteLine(output.Target);
            for (var i = from; i < count; i++)
            {
                var div3 = i % 3 == 0;
                var div5 = i % 5 == 0;
                if (div3 && div5)
                {
                    output("FizzBuzz");
                }
                else if (div3)
                {
                    //Same
                    output.Invoke("Fizz");

                }
                else if (div5)
                {
                    output("Buzz");
                }
                else
                {
                    output(i.ToString());
                }
            }
        }
    }
}