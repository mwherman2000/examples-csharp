using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace Transactions3Small
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
            //int dobOffset = len - 4;
            int dobOffset = len;
            for (int bytesOffset = 0; bytesOffset < len; bytesOffset += 4)
            {
                //Console.WriteLine("bytesOffset " + bytesOffset.ToString());
                //Console.WriteLine("dobOffset " + dobOffset.ToString());
                Runtime.Notify("bytesOffset ", bytesOffset);

                //dobBytes[dobOffset + 3] = bytes[bytesOffset + 0];  // NEO Debugger is throwing an exception here
                //dobBytes[dobOffset + 2] = bytes[bytesOffset + 1];  // This runs fine under Windows (check out the TransAsDisplayOrderBytesTest1 project)
                //dobBytes[dobOffset + 1] = bytes[bytesOffset + 2];
                //dobBytes[dobOffset + 0] = bytes[bytesOffset + 3];

                byte b0 = bytes[bytesOffset + 0];
                byte b1 = bytes[bytesOffset + 1];
                byte b2 = bytes[bytesOffset + 2];
                byte b3 = bytes[bytesOffset + 3];

                //dobBytes[dobOffset + 3] = b0;  // NEO Debugger is throwing an exception here
                //dobBytes[dobOffset + 2] = b1;  
                //dobBytes[dobOffset + 1] = b2;
                //dobBytes[dobOffset + 0] = b3;
                //dobOffset -= 4;

                dobOffset--;
                Runtime.Notify("dobOffset ", dobOffset);
                dobBytes[dobOffset] = b0;       // NEO Debugger is throwing an exception here
                dobOffset--;
                Runtime.Notify("dobOffset ", dobOffset);
                dobBytes[dobOffset] = b1;
                dobOffset--;
                Runtime.Notify("dobOffset ", dobOffset);
                dobBytes[dobOffset] = b2;
                dobOffset--;
                Runtime.Notify("dobOffset ", dobOffset);
                dobBytes[dobOffset] = b3;
            }
            return dobBytes;
        }
    }

    public class Transactions3Small : SmartContract
    {

        public static void Main()
        {
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsString()", ((BigInteger)0x0123456789abcdef).AsByteArray().AsString());
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes()", ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes());
        }
    }
}
