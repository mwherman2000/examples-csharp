using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesString
{
    public class ContractSimpleTypesDouble : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        string _string;

        public string _public_string;
        public static string _public_static_string;
        //NOTVS6 public const string _public_const_string;

        private string _private_string;
        private static string _private_static_string;
        //NOTVS6 private const string _private_const_string;

        protected string _protected_string;
        protected static string _protected_static_string;
        //NOTVS6 protected const string _protected_const_string;

        string _string_init = "1";

        public string _public_string_init = "1";
        public static string _public_static_string_init = "2";
        public const string _public_const_string_init = "3";

        private string _private_string_init = "1";
        private static string _private_static_string_init = "2";
        private const string _private_const_string_init = "3";

        protected string _protected_string_init = "1";
        protected static string _protected_static_string_init = "2";
        protected const string _protected_const_string_init = "3";

        public static void Main()
        {
            // Main() scoped
            string _main_string;

            //NOTVS6 public string _main_public_string;
            //NOTVS6 public static string _main_public_static_string;
            //NOTVS6 public const string _main_public_const_string;

            //NOTVS6 private string _main_private_string;
            //NOTVS6 private static string _main_private_static_string;
            //NOTVS6 private const string _main_private_const_string;

            //NOTVS6 protected string _main_protected_string;
            //NOTVS6 protected static string _main_protected_static_string;
            //NOTVS6 protected const string _main_protected_const_string;

            string _main_string_init = "1";

            //NOTVS6 public string _main_public_string_init = "1";
            //NOTVS6 public static string _main_public_static_string_init = "2";
            //NOTVS6 public const string _main_public_const_string_init = "3";

            //NOTVS6 private string _main_private_string_init = "1";
            //NOTVS6 private static string _main_private_static_string_init = "2";
            //NOTVS6 private const string _main_private_const_string_init = "3";

            //NOTVS6 protected string _main_protected_string_init = "1";
            //NOTVS6 protected static string _main_protected_static_string_init = "2";
            //NOTVS6 protected const string _main_protected_const_string_init = "3";

            // Class scoped
            //NOTVS6 _string = "1";

            //NOTVS6 _public_string = "1";
            _public_static_string = "2";
            //NOTVS6 _public_const_string = "3";

            //NOTVS6 _private_string = "1";
            _private_static_string = "2";
            //NOTVS6 _private_const_string = "3";

            //NOTVS6 _protected_string = "1";
            _protected_static_string = "2";
            //NOTVS6 _protected_const_string = "3";

            //NOTVS6 _string_init = "1";

            //NOTVS6 _public_string_init = "1";
            _public_static_string_init = "2";
            //NOTVS6 _public_const_string_init = "3";

            //NOTVS6 _private_string_init = "1";
            _private_static_string_init = "2";
            //NOTVS6 _private_const_string_init = "3";

            //NOTVS6 _protected_string_init = "1";
            _protected_static_string_init = "2";
            //NOTVS6 _protected_const_string_init = "3";

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
