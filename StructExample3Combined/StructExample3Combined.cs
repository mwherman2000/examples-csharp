using System;
using Neo.SmartContract.Framework;

namespace StructExample
{
    public class Point
    {
        public int X;
        public int Y;

        // class name and property names
        private const string _className ="Point";
        private const string _sX = "X";
        private const string _sY = "Y";
        private static readonly byte[] _bX = Helper.AsByteArray(_sX);
        private static readonly byte[] _bY = Helper.AsByteArray(_sY);

        // internal 
        private const string _classKeyTag = "/#" + _className + ".";
        private static readonly byte[] _bclassKeyTag = Helper.AsByteArray(_classKeyTag);

        public Point() // return a "null" Point
        {
            X = Int32.MaxValue;
            Y = Int32.MinValue;
        }

        public Point(int x, int y) // return an initialized Point
        {
            X = x;
            Y = y;
        }

        public static Point Null()
        {
            Point p = new Point(); // null Point by default
            return p;
        }

        public static bool IsNull(Point p)
        {
            bool flag = false;
            Point nullp = Null();
            if (p.X == nullp.X && p.Y == nullp.Y) flag = true;
            return flag;
        }

        public static bool Put(byte[] key, Point p)
        {
            if (key.Length == 0) return false;

            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bX), p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(_bkeyTag, _bY), p.Y);
            return true;
        }

        public static bool Put(string key, Point p)
        {
            if (key.Length == 0) return false;

            string _skeyTag = key + _classKeyTag;

            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sX, p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sY, p.Y);
            return true;
        }

        public static Point Get(byte[] key)
        {
            if (key.Length == 0) return Null();

            byte[] _bkeyTag = Helper.Concat(key, _bclassKeyTag);

            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, _bX)).AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, _bY)).AsBigInteger();
            Point p = new Point();
            p.X = x;
            p.Y = y;
            return p;
        }

        public static Point Get(string key)
        {
            if (key.Length == 0) return Null();

            string _skeyTag = key + _classKeyTag;

            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sX).AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, _skeyTag + _sY).AsBigInteger();
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
