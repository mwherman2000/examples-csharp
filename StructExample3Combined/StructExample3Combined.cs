using Neo.SmartContract.Framework;

namespace StructExample
{
    public class Point
    {
        public int X;
        public int Y;

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static void Put(byte[] key, Point p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Point.X")), p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Point.Y")), p.Y);
        }

        public static void Put(string key, Point p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Point.X", p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Point.Y", p.Y);
        }

        public static Point Get(byte[] key)
        {
            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Point.X"))).AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Point.Y"))).AsBigInteger();
            Point p = new Point();
            p.X = x;
            p.Y = y;
            return p;
        }

        public static Point Get(string key)
        {
            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Point.X").AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Point.Y").AsBigInteger();
            Point p = new Point();
            p.X = x;
            p.Y = y;
            return p;
        }
    }

    public class StructExample3Combined : SmartContract
    {
        private static Point Add(Point a, Point b)
        {
            return new Point
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Point Main()
        {
            Point p1 = new Point
            {
                X = 1,
                Y = 2
            };
            Point p2 = new Point
            {
                X = 2,
                Y = 1
            };

            Point[] array = new[]
            {
                p1, p2
            };

            Point p3 = Add(array[0], array[1]);

            Point.Put("p1", p1);
            Point.Put("p2", p2);
            Point.Put("p3", p3);

            Point p1get = Point.Get("p1");
            Point p2get = Point.Get("p2");
            Point p3get = Point.Get("p3");

            return p3get;
        }
    }
}
