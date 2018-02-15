using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace Transactions1
{
    public static class NeoExtensionMethods
    {
        private const int BUFSIZE = 200;
        static byte[] dobBytes = new byte[BUFSIZE];

        public static byte[] AsDisplayOrderBytes(this byte[] bytes)
        {
            //byte[] dobBytes = new byte[bytes.Length];
            //byte[] dobBytes = (byte[])bytes.Clone();
            //byte[] dobBytes = new byte[BUFSIZE];

            int len = bytes.Length;
            //Console.WriteLine("len " + len.ToString());
            Runtime.Notify("len ", len);
            if (len > BUFSIZE) len = BUFSIZE;
            //Console.WriteLine("len " + len.ToString());
            Runtime.Notify("len ", len);
            int dobOffset = len - 4;
            for (int bytesOffset = 0; bytesOffset < len; bytesOffset += 4)
            {
                //Console.WriteLine("bytesOffset " + bytesOffset.ToString());
                //Console.WriteLine("dobOffset " + dobOffset.ToString());
                Runtime.Notify("bytesOffset ", bytesOffset);
                Runtime.Notify("dobOffset ", dobOffset);
                dobBytes[dobOffset + 3] = bytes[bytesOffset + 0];
                dobBytes[dobOffset + 2] = bytes[bytesOffset + 1];
                dobBytes[dobOffset + 1] = bytes[bytesOffset + 2];
                dobBytes[dobOffset + 0] = bytes[bytesOffset + 3];
                dobOffset -= 4;
            }
            return dobBytes;
        }
    }

    public class Transactions1Small : SmartContract
    {

        public static void Main()
        {
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsString()", ((BigInteger)0x0123456789abcdef).AsByteArray().AsString());
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes()", ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes());
        }
    }
}
