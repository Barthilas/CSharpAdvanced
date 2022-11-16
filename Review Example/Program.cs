namespace Review_Example
{
    //params can be used with delegates.
    delegate void Hey(params object[] args);
    class Base
    {
        public virtual event Action Hey;
        //Cannot be overloaded.
        //public virtual event Action<int> Hey;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            InTransaction(() =>
            {
                Console.WriteLine("DOING STUFF IN THE DATABASE");
            });
            InTransaction(() =>
            {
                Console.WriteLine("DOING STUFF IN THE DATABASE");
            });
            InTransaction(() =>
            {
                Console.WriteLine("DOING STUFF IN THE DATABASE");
            });
        }

        static void InTransaction(Action action)
        {
            // does a bunch crazy database transaction logic
            Console.WriteLine("Init transaction");
            action();
            //either commits the transaction if an exception wasnt caught otherwise rolls it back.
            Console.WriteLine("Clean up transaction");
        }
    }
}