using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Strings2
{
    public class Contract1 : SmartContract
    {
        public static void Main()
        {
            string str = "Hello";
            str += " There";
            Runtime.Notify(str);
        }
    }
}
