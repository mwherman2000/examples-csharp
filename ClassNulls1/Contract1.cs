using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace ClassNulls1
{
    public class Point
    {
        public BigInteger x;
        public BigInteger y;
    }

    public class Contract1 : SmartContract
    {
        public static string Main()
        {
            string message = "";

            Point p = new Point();
            p.x = 1;
            p.y = 2;
            //if (p != null) message += "p != null worked. ";
            //if (p == null) message += "p == null worked. ";

            Point nullp = null;
            if (nullp == null) message += "nullp == null worked. ";
            if (nullp != null) message += "nullp != null worked. ";

            nullp = new Point();
            nullp.x = 3;
            nullp.y = 4;

            //if (nullp == null) message += "nullp == null worked. ";
            //if (nullp != null) message += "nullp != null worked. ";

            return message;
        }
    }
}
