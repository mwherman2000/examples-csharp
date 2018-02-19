using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesUInit
{
    public class ContractSimpleTypesUInt : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        uint _uint;

        public uint _public_uint;
        public static uint _public_static_uint;
        //NOTVS6 public const uint _public_const_uint;

        private uint _private_uint;
        private static uint _private_static_uint;
        //NOTVS6 private const uint _private_const_uint;

        protected uint _protected_uint;
        protected static uint _protected_static_uint;
        //NOTVS6 protected const uint _protected_const_uint;

        uint _uint_init = 1;

        public uint _public_uint_init = 1;
        public static uint _public_static_uint_init = 2;
        public const uint _public_const_uint_init = 3;

        private uint _private_uint_init = 1;
        private static uint _private_static_uint_init = 2;
        private const uint _private_const_uint_init = 3;

        protected uint _protected_uint_init = 1;
        protected static uint _protected_static_uint_init = 2;
        protected const uint _protected_const_uint_init = 3;

        public static void Main()
        {
            // Main() scoped
            uint _main_uint;

            //NOTVS6 public uint _main_public_uint;
            //NOTVS6 public static uint _main_public_static_uint;
            //NOTVS6 public const uint _main_public_const_uint;

            //NOTVS6 private uint _main_private_uint;
            //NOTVS6 private static uint _main_private_static_uint;
            //NOTVS6 private const uint _main_private_const_uint;

            //NOTVS6 protected uint _main_protected_uint;
            //NOTVS6 protected static uint _main_protected_static_uint;
            //NOTVS6 protected const uint _main_protected_const_uint;

            uint _main_uint_init = 1;

            //NOTVS6 public uint _main_public_uint_init = 1;
            //NOTVS6 public static uint _main_public_static_uint_init = 2;
            //NOTVS6 public const uint _main_public_const_uint_init = 3;

            //NOTVS6 private uint _main_private_uint_init = 1;
            //NOTVS6 private static uint _main_private_static_uint_init = 2;
            //NOTVS6 private const uint _main_private_const_uint_init = 3;

            //NOTVS6 protected uint _main_protected_uint_init = 1;
            //NOTVS6 protected static uint _main_protected_static_uint_init = 2;
            //NOTVS6 protected const uint _main_protected_const_uint_init = 3;

            // Class scoped
            //NOTVS6 _uint = 1;

            //NOTVS6 _public_uint = 1;
            //NOTNEO _public_static_uint = 2;
            //NOTVS6 _public_const_uint = 3;

            //NOTVS6 _private_uint = 1;
            //NOTNEO _private_static_uint = 2;
            //NOTVS6 _private_const_uint = 3;

            //NOTVS6 _protected_uint = 1;
            //NOTNEO _protected_static_uint = 2;
            //NOTVS6 _protected_const_uint = 3;

            //NOTVS6 _uint_init = 1;

            //NOTVS6 _public_uint_init = 1;
            //NOTNEO _public_static_uint_init = 2;
            //NOTVS6 _public_const_uint_init = 3;

            //NOTVS6 _private_uint_init = 1;
            //NOTNEO _private_static_uint_init = 2;
            //NOTVS6 _private_const_uint_init = 3;

            //NOTVS6 _protected_uint_init = 1;
            //NOTNEO _protected_static_uint_init = 2;
            //NOTVS6 _protected_const_uint_init = 3;

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
