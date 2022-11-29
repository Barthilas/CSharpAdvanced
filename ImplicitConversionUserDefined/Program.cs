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
            public static implicit operator Vector2(Vector3 rhs)
            {
                return new Vector2(rhs.X, rhs.Y);
            }

            public static implicit operator Vector3(Vector2 rhs)
            {
                return new Vector3(rhs.X, rhs.Y, 0);
            }
        }
        static void Main(string[] args)
        {
            //Implicit conversion (being implied.)
            Hey(20);
            //Explicit conversion
            Hey((float) 20.0);


            var vector3 = GetInformationFromDatabase();
            //Manual conversion.
            SizeConsoleWindow(new Vector2(vector3.X, vector3.Y));
            //User defined conversion
            SizeConsoleWindow(vector3);

            Vector2 vect = new Vector3(1, 2, 3);
            var better = (Vector2)new Vector3(1, 2, 3);
            var pokus = (Vector2)(object)new Vector3(1, 2, 3);

            
        }

        static void Hey(float myFloat)
        {

        }

        static Vector3 GetInformationFromDatabase() { return new Vector3(1, 2, 3); }
        static void SizeConsoleWindow(Vector2 vector2) { }
    }
}