using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesByte
{
    public class ContractSimpleTypesBool : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        byte _byte;

        public byte _public_byte;
        public static byte _public_static_byte;
        //NOTVS6 public const byte _public_const_byte;

        private byte _private_byte;
        private static byte _private_static_byte;
        //NOTVS6 private const byte _private_const_byte;

        protected byte _protected_byte;
        protected static byte _protected_static_byte;
        //NOTVS6 protected const byte _protected_const_byte;

        byte _byte_init = 1;

        public byte _public_byte_init = 1;
        public static byte _public_static_byte_init = 2;
        public const byte _public_const_byte_init = 3;

        private byte _private_byte_init = 1;
        private static byte _private_static_byte_init = 2;
        private const byte _private_const_byte_init = 3;

        protected byte _protected_byte_init = 1;
        protected static byte _protected_static_byte_init = 2;
        protected const byte _protected_const_byte_init = 3;

        public static void Main()
        {
            // Main() scoped
            byte _main_byte;

            //NOTVS6 public byte _main_public_byte;
            //NOTVS6 public static byte _main_public_static_byte;
            //NOTVS6 public const byte _main_public_const_byte;

            //NOTVS6 private byte _main_private_byte;
            //NOTVS6 private static byte _main_private_static_byte;
            //NOTVS6 private const byte _main_private_const_byte;

            //NOTVS6 protected byte _main_protected_byte;
            //NOTVS6 protected static byte _main_protected_static_byte;
            //NOTVS6 protected const byte _main_protected_const_byte;

            byte _main_byte_init = 1;

            //NOTVS6 public byte _main_public_byte_init = 1;
            //NOTVS6 public static byte _main_public_static_byte_init = 2;
            //NOTVS6 public const byte _main_public_const_byte_init = 3;

            //NOTVS6 private byte _main_private_byte_init = 1;
            //NOTVS6 private static byte _main_private_static_byte_init = 2;
            //NOTVS6 private const byte _main_private_const_byte_init = 3;

            //NOTVS6 protected byte _main_protected_byte_init = 1;
            //NOTVS6 protected static byte _main_protected_static_byte_init = 2;
            //NOTVS6 protected const byte _main_protected_const_byte_init = 3;

            // Class scoped
            //NOTVS6 _byte = 1;

            //NOTVS6 _public_byte = 1;
            //NOTNEO _public_static_byte = 2;
            //NOTVS6 _public_const_byte = 3;

            //NOTVS6 _private_byte = 1;
            //NOTNEO _private_static_byte = 2;
            //NOTVS6 _private_const_byte = 3;

            //NOTVS6 _protected_byte = 1;
            //NOTNEO _protected_static_byte = 2;
            //NOTVS6 _protected_const_byte = 3;

            //NOTVS6 _byte_init = 1;

            //NOTVS6 _public_byte_init = 1;
            //NOTNEO _public_static_byte_init = 2;
            //NOTVS6 _public_const_byte_init = 3;

            //NOTVS6 _private_byte_init = 1;
            //NOTNEO _private_static_byte_init = 2;
            //NOTVS6 _private_const_byte_init = 3;

            //NOTVS6 _protected_byte_init = 1;
            //NOTNEO _protected_static_byte_init = 2;
            //NOTVS6 _protected_const_byte_init = 3;

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
