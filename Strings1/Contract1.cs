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

            string nullstring = null;
            if (nullstring == null) message += "nullstring == null worked. ";
            nullstring = "Hello World";
            message += nullstring;
            return message;
        }
    }
}
