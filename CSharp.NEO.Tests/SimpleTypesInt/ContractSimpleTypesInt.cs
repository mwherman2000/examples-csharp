using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesInt
{
    public class ContractSimpleTypesInt : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        int _int;

        public int _public_int;
        public static int _public_static_int;
        //NOTVS6 public const int _public_const_int;

        private int _private_int;
        private static int _private_static_int;
        //NOTVS6 private const int _private_const_int;

        protected int _protected_int;
        protected static int _protected_static_int;
        //NOTVS6 protected const int _protected_const_int;

        int _int_init = 1;

        public int _public_int_init = 1;
        public static int _public_static_int_init = 2;
        public const int _public_const_int_init = 3;

        private int _private_int_init = 1;
        private static int _private_static_int_init = 2;
        private const int _private_const_int_init = 3;

        protected int _protected_int_init = 1;
        protected static int _protected_static_int_init = 2;
        protected const int _protected_const_int_init = 3;

        public static void Main()
        {
            // Main() scoped
            int _main_int;

            //NOTVS6 public int _main_public_int;
            //NOTVS6 public static int _main_public_static_int;
            //NOTVS6 public const int _main_public_const_int;

            //NOTVS6 private int _main_private_int;
            //NOTVS6 private static int _main_private_static_int;
            //NOTVS6 private const int _main_private_const_int;

            //NOTVS6 protected int _main_protected_int;
            //NOTVS6 protected static int _main_protected_static_int;
            //NOTVS6 protected const int _main_protected_const_int;

            int _main_int_init = 1;

            //NOTVS6 public int _main_public_int_init = 1;
            //NOTVS6 public static int _main_public_static_int_init = 2;
            //NOTVS6 public const int _main_public_const_int_init = 3;

            //NOTVS6 private int _main_private_int_init = 1;
            //NOTVS6 private static int _main_private_static_int_init = 2;
            //NOTVS6 private const int _main_private_const_int_init = 3;

            //NOTVS6 protected int _main_protected_int_init = 1;
            //NOTVS6 protected static int _main_protected_static_int_init = 2;
            //NOTVS6 protected const int _main_protected_const_int_init = 3;

            // Class scoped
            //NOTVS6 _int = 1;

            //NOTVS6 _public_int = 1;
            //NOTNEO _public_static_int = 2;
            //NOTVS6 _public_const_int = 3;

            //NOTVS6 _private_int = 1;
            //NOTNEO _private_static_int = 2;
            //NOTVS6 _private_const_int = 3;

            //NOTVS6 _protected_int = 1;
            //NOTNEO _protected_static_int = 2;
            //NOTVS6 _protected_const_int = 3;

            //NOTVS6 _int_init = 1;

            //NOTVS6 _public_int_init = 1;
            //NOTNEO _public_static_int_init = 2;
            //NOTVS6 _public_const_int_init = 3;

            //NOTVS6 _private_int_init = 1;
            //NOTNEO _private_static_int_init = 2;
            //NOTVS6 _private_const_int_init = 3;

            //NOTVS6 _protected_int_init = 1;
            //NOTNEO _protected_static_int_init = 2;
            //NOTVS6 _protected_const_int_init = 3;

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
