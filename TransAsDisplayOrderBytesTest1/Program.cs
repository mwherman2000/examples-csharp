using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TransAsDisplayOrderBytesTest1
{
    public static class NeoExtensionMethods
    {
        private const int BUFSIZE = 100;
        static byte[] dobBytes = new byte[BUFSIZE];

        public static byte[] AsDisplayOrderBytes(this byte[] bytes)
        {
            //byte[] dobBytes = new byte[bytes.Length];
            //byte[] dobBytes = (byte[])bytes.Clone();
            //byte[] dobBytes = new byte[BUFSIZE];

            int len = bytes.Length;
            Console.WriteLine("len " + len.ToString());
            if (len > BUFSIZE) len = BUFSIZE;
            Console.WriteLine("len " + len.ToString());
            int dobOffset = len - 4;
            for (int bytesOffset = 0; bytesOffset < len; bytesOffset += 4)
            {
                Console.WriteLine("bytesOffset " + bytesOffset.ToString());
                Console.WriteLine("dobOffset " + dobOffset.ToString());
                dobBytes[dobOffset + 3] = bytes[bytesOffset + 0];
                dobBytes[dobOffset + 2] = bytes[bytesOffset + 1];
                dobBytes[dobOffset + 1] = bytes[bytesOffset + 2];
                dobBytes[dobOffset + 0] = bytes[bytesOffset + 3];
                dobOffset -= 4;
            }
            return dobBytes;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main: (0x0123456789abcdef).ToString(\"X\") " + (0x0123456789abcdef).ToString("X"));
            Console.WriteLine("Main: ((BigInteger)0x0123456789abcdef).ToByteArray() " + ByteToHexBitFiddle(((BigInteger)0x0123456789abcdef).ToByteArray()));
            Console.WriteLine("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes() " + ByteToHexBitFiddle(((BigInteger)0x0123456789abcdef).ToByteArray().AsDisplayOrderBytes()));
            Console.ReadLine();
        }

        // Credit: https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
        static string ByteToHexBitFiddle(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            int b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }
    }
}
