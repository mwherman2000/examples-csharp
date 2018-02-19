using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesDouble
{
    public class ContractSimpleTypesDouble : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        // System.Exception: error:System.Void SimpleTypesString.ContractSimpleTypesDouble::Main()::IL_000C Stsfld  ---> System.Exception: unsupported instruction Stsfld 

        double _double;

        public double _public_double;
        public static double _public_static_double;
        //NOTVS6 public const double _public_const_double;

        private double _private_double;
        private static double _private_static_double;
        //NOTVS6 private const double _private_const_double;

        protected double _protected_double;
        protected static double _protected_static_double;
        //NOTVS6 protected const double _protected_const_double;

        double _double_init = 1.0;

        public double _public_double_init = 1.0;
        public static double _public_static_double_init = 2.0;
        public const double _public_const_double_init = 3.0;

        private double _private_double_init = 1.0;
        private static double _private_static_double_init = 2.0;
        private const double _private_const_double_init = 3.0;

        protected double _protected_double_init = 1.0;
        protected static double _protected_static_double_init = 2.0;
        protected const double _protected_const_double_init = 3.0;

        public static void Main()
        {
            // Main() scoped
            //NONEO double _main_double;

            //NOTVS6 public double _main_public_double;
            //NOTVS6 public static double _main_public_static_double;
            //NOTVS6 public const double _main_public_const_double;

            //NOTVS6 private double _main_private_double;
            //NOTVS6 private static double _main_private_static_double;
            //NOTVS6 private const double _main_private_const_double;

            //NOTVS6 protected double _main_protected_double;
            //NOTVS6 protected static double _main_protected_static_double;
            //NOTVS6 protected const double _main_protected_const_double;

            //NONEO double _main_double_init = 1.0;

            //NOTVS6 public double _main_public_double_init = 1.0;
            //NOTVS6 public static double _main_public_static_double_init = 2.0;
            //NOTVS6 public const double _main_public_const_double_init = 3.0;

            //NOTVS6 private double _main_private_double_init = 1.0;
            //NOTVS6 private static double _main_private_static_double_init = 2.0;
            //NOTVS6 private const double _main_private_const_double_init = 3.0;

            //NOTVS6 protected double _main_protected_double_init = 1.0;
            //NOTVS6 protected static double _main_protected_static_double_init = 2.0;
            //NOTVS6 protected const double _main_protected_const_double_init = 3.0;

            // Class scoped
            //NOTVS6 _double = 1.0;

            //NOTVS6 _public_double = 1.0;
            //NOTNEO _public_static_double = 2.0;
            //NOTVS6 _public_const_double = 3.0;

            //NOTVS6 _private_double = 1.0;
            //NOTNEO _private_static_double = 2.0;
            //NOTVS6 _private_const_double = 3.0;

            //NOTVS6 _protected_double = 1.0;
            //NOTNEO _protected_static_double = 2.0;
            //NOTVS6 _protected_const_double = 3.0;

            //NOTVS6 _double_init = 1.0;

            //NOTVS6 _public_double_init = 1.0;
            //NOTNEO _public_static_double_init = 2.0;
            //NOTVS6 _public_const_double_init = 3.0;

            //NOTVS6 _private_double_init = 1.0;
            //NOTNEO _private_static_double_init = 2.0;
            //NOTVS6 _private_const_double_init = 3.0;

            //NOTVS6 _protected_double_init = 1.0;
            //NOTNEO _protected_static_double_init = 2.0;
            //NOTVS6 _protected_const_double_init = 3.0;

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
