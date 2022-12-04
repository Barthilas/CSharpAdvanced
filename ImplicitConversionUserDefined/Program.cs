namespace ImplicitConversionUserDefined
{
    internal class Program
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

            //Doesnt matter if its defined in vector2 or 3.
            // Operators only work during compile time. They do not work during runtime.
            // !!!! commented out for design how it should be correctly. Based on (loss of data thought) !!!!
            //public static implicit operator Vector2(Vector3 rhs)
            //{
            //    return new Vector2(rhs.X, rhs.Y);
            //}

            public static implicit operator Vector3(Vector2 rhs)
            {
                return new Vector3(rhs.X, rhs.Y, 0);
            }

            public static explicit operator Vector2(Vector3 rhs)
            {
                return new Vector2(rhs.X, rhs.Y);
            }

            //public static explicit operator Vector3(Vector2 rhs)
            //{
            //    return new Vector3(rhs.X, rhs.Y, 0);
            //}

            //!!!!! NEVER DO THIS, BAD DESIGN !!!!! (Abuse of operators - this is parsing)
            // REASON: Input can be interpreted in so many other ways. We want logical 1:1 conversion. NOT TO USE IT FOR PARSING.
            public static implicit operator Vector3(string str)
            {
                //parse string input
                return new Vector3(1, 2, 3);
            }

            //Stupid stuff as well, makes no sense.
            public static implicit operator int(Vector3 rhs)
            {
                return 10;
            }
        }
        static void Main(string[] args)
        {
            //Implicit conversion (being implied.)
            Hey(20);
            //Explicit conversion
            Hey((float)20.0);


            var vector3 = GetInformationFromDatabase();
            //Manual conversion.
            SizeConsoleWindow(new Vector2(vector3.X, vector3.Y));
            //User defined conversion
            SizeConsoleWindow(vector3);

            //implicit Conversion works for explicitly aswell.
            //!!!!!!!!!!!!!!!!  Only do implicit conversion when there is no loss of data.  !!!!!!!!!!!!!!!!
            Vector2 vect = new Vector3(1, 2, 3);

            // Compile: what is the type of vector3?
            // Compile: Does the Compile time type of vector3 have conversion?
            // Compile: Indeed it does.
            var better = (Vector2)new Vector3(1, 2, 3);
            //RunTime: var better = Vector3.explicit_operator_vector2(new Vector3(1,1,1))
            
            var pokus = (Vector2)(object)new Vector3(1, 2, 3); //fails at runtime without conversions.

            //explicit example
            var vect3 = new Vector3(1, 1, 1);
            Blegh((Vector2)vect3);


        }

        static void Hey(float myFloat) { }
        static void Blegh(Vector2 hey) { }

        static void Blegh(object whoa)
        {
            // Compile: what is the type of whoa?
            // Compile: Does the Compile time type of whoa have conversion?
            // Compile: It's an object, so no.
            var vector2 = (Vector2)whoa;
            vector2.X;
            //R: if (!(whoa is Vector2))
            //R:    throw new InvalidCastException()
            //R:vector2 = whoa;
        }

        static void BleghValid(Vector2 vector2)
        {
            Console.Writeline(vector2.x);
        }
        static Vector3 GetInformationFromDatabase() { return new Vector3(1, 2, 3); }
        static void SizeConsoleWindow(Vector2 vector2) { }
    }
}