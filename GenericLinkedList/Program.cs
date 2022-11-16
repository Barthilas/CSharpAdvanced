namespace GenericLinkedList
{
    interface IMyInterface
    {
        void Method();
    }
    class LinkedList<T> /*where T: IMyInterface*/ /* where T : class*/ /* where T : struct */ 
    {
        class Node
        {
            public T value;
            public Node Next;
        }

        private Node _first;
        public int Count { get; private set; }
        public void Add(T item)
        {
            var node = new Node();
            // works with T: IMyInterface because its guaranteed its going to have Method.
            //node.value.Method();
            node.value = default(T);
        }
        public void Remove(T item) { }
        public T Get(int index) {
            //return null;
            // reference type = null
            // value = call constructor
            return default(T); 
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //works with T : struct -> struct is value type
            //var ll1 = new LinkedList<int>();

            //works with T : class because string is reference type.
            var ll = new LinkedList<string>();
        }
    }
}