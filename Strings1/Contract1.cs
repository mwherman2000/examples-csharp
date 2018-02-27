using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Strings1
{
    public class Contract1 : SmartContract
    {
        public static string Main()
        {
            string message = "";

            //string empty = "";
            //string nullstring = null;
            //if (String.IsNullOrWhiteSpace(empty)) message += "String.IsNullOrWhiteSpace(empty) worked. ";
            //if (String.IsNullOrEmpty(empty)) message += "String.IsNullOrEmpty(empty) worked. ";
            //if (String.IsNullOrEmpty(nullstring)) message += "String.IsNullOrEmpty(nullstring) worked. ";

            string nullstring2 = null;
            if (nullstring2 == null) message += "nullstring2 == null worked. ";
            nullstring2 = "Hello World";
            message += nullstring2;
            return message;
        }
    }
}
