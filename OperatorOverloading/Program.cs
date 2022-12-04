using System;

namespace OperatorOverloading
{
    class Program
    {
        class Vector2
        {
            public Vector2(float x, float y)
            {
                X = x;
                Y = y;
            }

            public float X { get; set; }
            public float Y { get; set; }
        }
        class Vector3
        {
            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
            {
                return new Vector3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
            }

            public static Vector3 operator *(Vector3 lhs, int rhs)
            {
                return new Vector3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
            }

            //error: must take in one of the containing type.
            //public static Vector3 operator *(int lhs, int rhs)
            //{
            //    return new Vector3(0, 0, 0);
            //}
            public static string operator *(int lhs, Vector3 rhs)
            {
                return "hey";
            }

            //cannot be done viz. main
            //public static Vector3 operator *= (Vector3 lhs, int rhs) { }

            public static Vector3 operator ++(Vector3 lhs)
            {
                return new Vector3(lhs.X + 1, lhs.Y + 1, lhs.Z + 1);
            }

            //false must be defined aswell
            public static bool operator true(Vector3 lhs)
            {
                return false;
            }

            public static bool operator false(Vector3 lhs)
            {
                return true;
            }

        }
        static void Main(string[] args)
        {
            //This is dumb.
            //var invoice = customer + order;

            //Overloadable unary: +, -, ~, |, ++, --, true, false
            //Binary: +, -, *, /, ^, <<, >>, &, |
            //Conditional: ==, !=, <, >, >=, <=

            //Nonoverloadable: =, [], (), pointer, sizeof, typeof

            var vect1 = new Vector3(1, 2, 3);
            var vect2 = new Vector3(2, 3, 4);
            var sum = vect1 + vect2;

            var whoa = vect1 * 2;
            var hey = 2 * vect2;
            Console.WriteLine(whoa);
            Console.WriteLine(hey);

            vect1 *= 20;
            //vect1 = vect1 * 20 thats why.

            if (vect1) // vect1 == false no conversion
                Console.WriteLine("whoa");
            else
                Console.WriteLine("HEY");

            Console.WriteLine(sum.X);

        }
    }
}
