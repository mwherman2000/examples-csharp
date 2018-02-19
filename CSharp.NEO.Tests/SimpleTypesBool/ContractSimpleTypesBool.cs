using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesBool
{
    public class ContractSimpleTypesBool : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        bool _bool;

        public bool _public_bool;
        public static bool _public_static_bool;
        //NOTVS6 public const bool _public_const_bool;

        private bool _private_bool;
        private static bool _private_static_bool;
        //NOTVS6 private const bool _private_const_bool;

        protected bool _protected_bool;
        protected static bool _protected_static_bool;
        //NOTVS6 protected const bool _protected_const_bool;

        bool _bool_init = true;

        public bool _public_bool_init = true;
        public static bool _public_static_bool_init = true;
        public const bool _public_const_bool_init = true;

        private bool _private_bool_init = true;
        private static bool _private_static_bool_init = true;
        private const bool _private_const_bool_init = true;

        protected bool _protected_bool_init = true;
        protected static bool _protected_static_bool_init = true;
        protected const bool _protected_const_bool_init = true;

        public static void Main()
        {
            // Main() scoped
            bool _main_bool;

            //NOTVS6 public bool _main_public_bool;
            //NOTVS6 public static bool _main_public_static_bool;
            //NOTVS6 public const bool _main_public_const_bool;

            //NOTVS6 private bool _main_private_bool;
            //NOTVS6 private static bool _main_private_static_bool;
            //NOTVS6 private const bool _main_private_const_bool;

            //NOTVS6 protected bool _main_protected_bool;
            //NOTVS6 protected static bool _main_protected_static_bool;
            //NOTVS6 protected const bool _main_protected_const_bool;

            bool _main_bool_init = true;

            //NOTVS6 public bool _main_public_bool_init = true;
            //NOTVS6 public static bool _main_public_static_bool_init = true;
            //NOTVS6 public const bool _main_public_const_bool_init = true;

            //NOTVS6 private bool _main_private_bool_init = true;
            //NOTVS6 private static bool _main_private_static_bool_init = true;
            //NOTVS6 private const bool _main_private_const_bool_init = true;

            //NOTVS6 protected bool _main_protected_bool_init = true;
            //NOTVS6 protected static bool _main_protected_static_bool_init = true;
            //NOTVS6 protected const bool _main_protected_const_bool_init = true;

            // Class scoped
            //NOTVS6 _bool = true;

            //NOTVS6 _public_bool = true;
            //NOTNEO _public_static_bool = true;
            //NOTVS6 _public_const_bool = true;

            //NOTVS6 _private_bool = true;
            //NOTNEO _private_static_bool = true;
            //NOTVS6 _private_const_bool = true;

            //NOTVS6 _protected_bool = true;
            //NOTNEO _protected_static_bool = true;
            //NOTVS6 _protected_const_bool = true;

            //NOTVS6 _bool_init = true;

            //NOTVS6 _public_bool_init = true;
            //NOTNEO _public_static_bool_init = true;
            //NOTVS6 _public_const_bool_init = true;

            //NOTVS6 _private_bool_init = true;
            //NOTNEO _private_static_bool_init = true;
            //NOTVS6 _private_const_bool_init = true;

            //NOTVS6 _protected_bool_init = true;
            //NOTNEO _protected_static_bool_init = true;
            //NOTVS6 _protected_const_bool_init = true;

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
