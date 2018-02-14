using Neo.SmartContract.Framework;

namespace StructExample
{
    public class Point2
    {
        public int X;
        public int Y;

        public Point2()
        {
        }

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static void Put(byte[] key, Point2 p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#X")), p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Y")), p.Y);
        }

        public static void Put(string key, Point2 p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#X", p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Y", p.Y);
        }

        public static Point2 Get(byte[] key)
        {
            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#X"))).AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Y"))).AsBigInteger();
            Point2 p = new Point2();
            p.X = x;
            p.Y = y;
            return p;
        }

        public static Point2 Get(string key)
        {
            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#X").AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Y").AsBigInteger();
            Point2 p = new Point2();
            p.X = x;
            p.Y = y;
            return p;
        }
    }

    public class StructExample3Combined : SmartContract
    {
        private static Point2 Add(Point2 a, Point2 b)
        {
            return new Point2
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static Point2 Main()
        {
            Point2 p1 = new Point2
            {
                X = 1,
                Y = 2
            };
            Point2 p2 = new Point2
            {
                X = 2,
                Y = 1
            };

            Point2[] array = new[]
            {
                p1, p2
            };

            Point2 p3 = Add(array[0], array[1]);

            Point2.Put("p1", p1);
            Point2.Put("p2", p2);
            Point2.Put("p3", p3);

            Point2 p1get = Point2.Get("p1");
            Point2 p2get = Point2.Get("p2");
            Point2 p3get = Point2.Get("p3");

            return p3get;
        }
    }
}
