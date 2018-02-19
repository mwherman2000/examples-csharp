using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace SimpleTypesChar
{
    public class ContractSimpleTypesChar : SmartContract
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types

        //NOTVS6  OpCode Stsfld - Replaces the value of a static field with a value from the evaluation stack
        //        https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.stsfld(v=vs.110).aspx

        char _char;

        public char _public_char;
        public static char _public_static_char;
        //NOTVS6 public const char _public_const_char;

        private char _private_char;
        private static char _private_static_char;
        //NOTVS6 private const char _private_const_char;

        protected char _protected_char;
        protected static char _protected_static_char;
        //NOTVS6 protected const char _protected_const_char;

        char _char_init = 'a';

        public char _public_char_init = 'a';
        public static char _public_static_char_init = 'b';
        public const char _public_const_char_init = 'c';

        private char _private_char_init = 'a';
        private static char _private_static_char_init = 'b';
        private const char _private_const_char_init = 'c';

        protected char _protected_char_init = 'a';
        protected static char _protected_static_char_init = 'b';
        protected const char _protected_const_char_init = 'c';

        public static void Main()
        {
            // Main() scoped
            char _main_char;

            //NOTVS6 public char _main_public_char;
            //NOTVS6 public static char _main_public_static_char;
            //NOTVS6 public const char _main_public_const_char;

            //NOTVS6 private char _main_private_char;
            //NOTVS6 private static char _main_private_static_char;
            //NOTVS6 private const char _main_private_const_char;

            //NOTVS6 protected char _main_protected_char;
            //NOTVS6 protected static char _main_protected_static_char;
            //NOTVS6 protected const char _main_protected_const_char;

            char _main_char_init = 'a';

            //NOTVS6 public char _main_public_char_init = 'a';
            //NOTVS6 public static char _main_public_static_char_init = 'b';
            //NOTVS6 public const char _main_public_const_char_init = 'c';

            //NOTVS6 private char _main_private_char_init = 'a';
            //NOTVS6 private static char _main_private_static_char_init = 'b';
            //NOTVS6 private const char _main_private_const_char_init = 'c';

            //NOTVS6 protected char _main_protected_char_init = 'a';
            //NOTVS6 protected static char _main_protected_static_char_init = 'b';
            //NOTVS6 protected const char _main_protected_const_char_init = 'c';

            // Class scoped
            //NOTVS6 _char = 'a';

            //NOTVS6 _public_char = 'a';
            //NOTNEO _public_static_char = 'b';
            //NOTVS6 _public_const_char = 'c';

            //NOTVS6 _private_char = 'a';
            //NOTNEO _private_static_char = 'b';
            //NOTVS6 _private_const_char = 'c';

            //NOTVS6 _protected_char = 'a';
            //NOTNEO _protected_static_char = 'b';
            //NOTVS6 _protected_const_char = 'c';

            //NOTVS6 _char_init = 'a';

            //NOTVS6 _public_char_init = 'a';
            //NOTNEO _public_static_char_init = 'b';
            //NOTVS6 _public_const_char_init = 'c';

            //NOTVS6 _private_char_init = 'a';
            //NOTNEO _private_static_char_init = 'b';
            //NOTVS6 _private_const_char_init = 'c';

            //NOTVS6 _protected_char_init = 'a';
            //NOTNEO _protected_static_char_init = 'b';
            //NOTVS6 _protected_const_char_init = 'c';

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
