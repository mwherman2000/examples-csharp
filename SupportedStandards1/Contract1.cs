using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SupportedStandards1
{
    public class Contract1 : SmartContract
    {
        private static readonly string[] _supportedStandards1 = { "NEP-5", "NEP-1234" };

        public static object Main(string operation, params object[] args)
        {
            object result = false;

            string[] _supportedStandards2 = { "NEP-5", "NEP-1234" };

            Runtime.Notify("_supportedStandards1", _supportedStandards1);
            Runtime.Notify("_supportedStandards2", _supportedStandards2);

            if (operation == "supportedStandards")
            {
                result = (object)supportedStandards();
            }

            Runtime.Notify("result", result);

            return result;
        }

        public static string[] supportedStandards()
        {
            string[] result;

            result = _supportedStandards1;

            Runtime.Notify("result1", result);

            return result;
        }
    }
}
