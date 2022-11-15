/*FizzBuzz
 * input i
 * i%3==0 Fizz
 * i%5==0 Buzz
 * Both FizzBuzz
*/
namespace CSharpAdvanced
{
    interface IFizzOutput
    {
        void Write(string output);
    }
    class FizzBuzz
    {
        private readonly IFizzOutput _output;

        public FizzBuzz(IFizzOutput output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public void Run(int from, int count)
        {
            for (var i = from; i < count; i++)
            {
                var div3 = i % 3 == 0;
                var div5 = i % 5 == 0;
                if (div3 && div5)
                {
                    _output.Write("FizzBuzz");
                }
                else if (div3)
                {
                    _output.Write("Fizz");

                }
                else if (div5)
                {
                    _output.Write("Buzz");
                }
                else
                {
                    _output.Write(i.ToString());
                }
            }
        }
    }

    class ConsoleFizzOutput : IFizzOutput
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzz = new FizzBuzz(new ConsoleFizzOutput());
            fizzBuzz.Run(0, 100);
        }
    }
}