using Neo.SmartContract.Framework;
using System;
using System.Numerics;

namespace StructExample
{
    public struct Point2
    {
        public int X;
        public int Y;

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Put(byte[] key)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#X")), this.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Y")), this.Y);
        }

        public static bool Put(string key, Point2 p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#X", p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Y", p.Y);
            return true;
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
}
