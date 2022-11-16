using System.Collections;

namespace Generics
{
    class Program
    {
        public class Stuff<T /*,T1,T2,T3, TKey, TValue..*/>
        {
            private readonly List<T> _keys;

            public Stuff()
            {
                _keys = new List<T>();
            }
            public void Method(T key)
            {
                _keys.Add(key);
            }
        }

        static void Main(string[] args)
        {
            //Only exception where type can be left out.
            var type = typeof(Stuff<>);

            var stuff = new Stuff<string>();
            //var ugly = new Stuff<Stuff<List<Stuff<int>>>>();
            stuff.Method("Hey");
        }
    }
}

//var items = new List<int>() { 1, 2, 3, 4, 5, 6 };
//var items2 = new List<float>() { 1.1f, 2.2f };

////items are object.. foreach is more costly as you need to cast.
//var arrayList = new ArrayList() { 1, 2, 3, 4, 5, 6 };

////equivalent to dictionary
//var hashtable = new Hashtable();