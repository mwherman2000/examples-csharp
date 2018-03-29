using Neo.SmartContract.Framework.Services.Neo;
using System;

namespace Neo.SmartContract
{
    public class HelloWorld : Framework.SmartContract
    {
        public static void Main()
        {
            Storage.Put(Storage.CurrentContext, "Hello", "World");

            int count = 1;
            Runtime.Notify("counta", count);

            Runtime.Notify("countb", count);
            count = 2;
            Runtime.Notify("countc", count);

            doSomething();

            if (true) goto TheEnd;

            Runtime.Notify("countd", count);
            count = 3;
            Runtime.Notify("counte", count);
            Runtime.Notify("countf", count);
            count = 4;
            Runtime.Notify("countg", count);

            Runtime.Notify("counth", count);
            count = 5;
            Runtime.Notify("counti", count);

            TheEnd: return;
        }

        private static void doSomething()
        {
            //goto TheEnd;
            //Exit();
            //Environment.Exit(0);
        }
    }
}
