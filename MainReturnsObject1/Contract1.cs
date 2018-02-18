using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace MainReturnsObject1
{
    public class Contract1 : SmartContract
    {
        public static Object Main(string operation, params object[] args)
        {
            Object obj = "unknown operation";

            // In C#.NEO, neon.exe is not always able to compile a switch() statement

            Runtime.Notify("operation", operation);

            if (operation == "int")
            {
                int i = (int)135;
                Runtime.Notify("i = (int)135", i);
                obj = i;
                Runtime.Notify("obj = i = (int)135", obj);
            }

            else if (operation == "BigInteger")
            {
                BigInteger bi = (BigInteger)246;
                Runtime.Notify("bi = (BigInteger)246", bi);
                obj = bi;
                Runtime.Notify("obj = bi = (BigInteger)246", obj);
            }

            else if (operation == "bytes")
            {
                byte[] bytes = { 0x00, 0x01, 0x02, 0x03 };
                Runtime.Notify("bytes", bytes);
                obj = bytes;
                Runtime.Notify("obj = bytes", obj);
            }

            else if (operation == "string")
            {
                string s = "Main returning obj";
                Runtime.Notify("s", s);
                obj = s;
                Runtime.Notify("obj = s = \"Main returning obj\"", obj);
            }

            else if (operation == "Hello")
            {
                Runtime.Notify("operation", operation);
                obj = operation + " world";
                Runtime.Notify("obj", obj);
            }

            return obj;
        }
    }
}
