using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesDecimal
{
    public class ContractSimpleTypesDecimal : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        // System.Exception: error:System.Void SimpleTypesDecimal.ContractSimpleTypesDecimal::Main()::IL_0001 Ldsfld  ---> System.NullReferenceException: Object reference not set to an instance of an object.
 

        decimal _decimal;

        public decimal _public_decimal;
        public static decimal _public_static_decimal;
        //NOTVS6 public const decimal _public_const_decimal;

        private decimal _private_decimal;
        private static decimal _private_static_decimal;
        //NOTVS6 private const decimal _private_const_decimal;

        protected decimal _protected_decimal;
        protected static decimal _protected_static_decimal;
        //NOTVS6 protected const decimal _protected_const_decimal;

        decimal _decimal_init = 1;

        public decimal _public_decimal_init = 1;
        public static decimal _public_static_decimal_init = 2;
        public const decimal _public_const_decimal_init = 3;

        private decimal _private_decimal_init = 1;
        private static decimal _private_static_decimal_init = 2;
        private const decimal _private_const_decimal_init = 3;

        protected decimal _protected_decimal_init = 1;
        protected static decimal _protected_static_decimal_init = 2;
        protected const decimal _protected_const_decimal_init = 3;

        public static void Main()
        {
            // Main() scoped
            decimal _main_decimal;

            //NOTVS6 public decimal _main_public_decimal;
            //NOTVS6 public static decimal _main_public_static_decimal;
            //NOTVS6 public const decimal _main_public_const_decimal;

            //NOTVS6 private decimal _main_private_decimal;
            //NOTVS6 private static decimal _main_private_static_decimal;
            //NOTVS6 private const decimal _main_private_const_decimal;

            //NOTVS6 protected decimal _main_protected_decimal;
            //NOTVS6 protected static decimal _main_protected_static_decimal;
            //NOTVS6 protected const decimal _main_protected_const_decimal;

            //NONEO decimal _main_decimal_init = 1;

            //NOTVS6 public decimal _main_public_decimal_init = 1;
            //NOTVS6 public static decimal _main_public_static_decimal_init = 2;
            //NOTVS6 public const decimal _main_public_const_decimal_init = 3;

            //NOTVS6 private decimal _main_private_decimal_init = 1;
            //NOTVS6 private static decimal _main_private_static_decimal_init = 2;
            //NOTVS6 private const decimal _main_private_const_decimal_init = 3;

            //NOTVS6 protected decimal _main_protected_decimal_init = 1;
            //NOTVS6 protected static decimal _main_protected_static_decimal_init = 2;
            //NOTVS6 protected const decimal _main_protected_const_decimal_init = 3;

            // Class scoped
            //NOTVS6 _decimal = 1;

            //NOTVS6 _public_decimal = 1;
            //NOTNEO _public_static_decimal = 2;
            //NOTVS6 _public_const_decimal = 3;

            //NOTVS6 _private_decimal = 1;
            //NOTNEO _private_static_decimal = 2;
            //NOTVS6 _private_const_decimal = 3;

            //NOTVS6 _protected_decimal = 1;
            //NOTNEO _protected_static_decimal = 2;
            //NOTVS6 _protected_const_decimal = 3;

            //NOTVS6 _decimal_init = 1;

            //NOTVS6 _public_decimal_init = 1;
            //NOTNEO _public_static_decimal_init = 2;
            //NOTVS6 _public_const_decimal_init = 3;

            //NOTVS6 _private_decimal_init = 1;
            //NOTNEO _private_static_decimal_init = 2;
            //NOTVS6 _private_const_decimal_init = 3;

            //NOTVS6 _protected_decimal_init = 1;
            //NOTNEO _protected_static_decimal_init = 2;
            //NOTVS6 _protected_const_decimal_init = 3;

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
