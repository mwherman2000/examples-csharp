using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace ReturnOperation1
{
    public class ReturnOperation1 : SmartContract
    {
        public static string Main(string operation, object[] args)
        {
            string message = operation + " world";
            Runtime.Log(message);
            Runtime.Notify(operation, message);
            return message;
        }
    }
}
