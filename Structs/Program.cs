using System;

namespace Structs
{
    //When to use?
    //Performance boost, when you know when to use them.
    interface IMyInter
    {
        void Hey();
    }
    /*internal*/
    //Structs are value types, like enums, integers, floats, doubles.
    //No null value.
    struct Vector2 : IMyInter //: Base //no inheritance for structures (exception interfaces).
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        //No default (parametless) constructor can be defined (it automaticaly exists for structs and sets properties to initial state).
        //public Vector2() { }

        //All Parameters must be defined.
        public Vector2(int x, int y) : this() //trick to use automatic properties.
        {
            X = x;
            Y = y;
        }

        //Never make mutable structs. Nuke this method and set "set" on properties to private.
        public void GoForward()
        {
            X += 20;
        }

        public void Hey()
        {
            Console.WriteLine("VECTOR 2!");
        }
    }
    class Whoa
    {
        public Vector2 Position { get; set; }

        //get: 
        //public Vector2 Get_Position()
    }
    class Program
    {
        static void Main(string[] args)
        {

            var vect2 = new Vector2(1, 2);
            Mutate(vect2);
            Console.WriteLine(vect2.X);

            var vect = new Vector2();
            vect.GoForward();
            Console.WriteLine(vect.X);

            var hey = new Whoa();
            //hey.Position.X = 20;
            hey.Position.GoForward();
            Console.WriteLine(hey.Position.X);

            //stack or heap? Implementation detail.
            var whoa = new Whoa();//heap
            Console.WriteLine(whoa.Position.X);

            var vect3 = new Vector2(2, 3);//stack
            Console.WriteLine(vect3.X);

            var vect4 = new Vector2(1, 2);
            var action = Hey();
            action();

            var vect6 = new Vector2(1, 2);
            MyMethod(vect6);

            Vector2? nullable = null;
            var vectorComp1 = new Vector2(2, 1);
            var vectorComp2 = new Vector2(2, 1);

            //if (vectorComp1 == vectorComp2)
            //    Console.WriteLine("EQUAL!");

        }
        static void MyMethod(IMyInter inter)
        {
            inter.Hey();
        }
        static Action Hey()
        {
            var vect5 = new Vector2(1, 2);

            Action act = () =>
            {
                Console.WriteLine(vect5.X);
            };

            return act;
        }

        //Receives a copy of Vector2. Passed by value.
        static void Mutate(Vector2 whoa)
        {
            whoa.X = 10;
        }
    }
}
