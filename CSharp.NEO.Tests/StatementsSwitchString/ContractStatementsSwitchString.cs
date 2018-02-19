using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace StatementsSwitchString
{
    public class ContractStatementsSwitchString : SmartContract
    {
        // System.Exception: error:System.Void SimpleTypesString.ContractSimpleTypesDouble::Main()::IL_000C Stsfld  ---> System.Exception: unsupported instruction Stsfld SimpleTypesString   C:\NEO\repos\NeoCSharpTests\SimpleTypesString\CONVERTTASK
        // System.Exception: error:System.Void SimpleTypesDecimal.ContractSimpleTypesDecimal::Main()::IL_0001 Ldsfld  ---> System.NullReferenceException: Object reference not set to an instance of an object.	SimpleTypesDecimal C:\NEO\repos\NeoCSharpTests\SimpleTypesDecimal\CONVERTTASK
        // System.InvalidOperationException: Stack empty.	SimpleTypesDouble C:\NEO\repos\NeoCSharpTests\SimpleTypesDouble\CONVERTTASK
        // System.Exception: error:System.Object StatementsSwitchString.ContractStatementsSwitchString::Main(System.String, System.Object[])::IL_0004 Call System.UInt32<PrivateImplementationDetails>::ComputeStringHash(System.String) ---> System.Exception: not supported on neovm now.StatementsSwitchString C:\NEO\repos\NeoCSharpTests\StatementsSwitchString\CONVERTTASK

        public static int Main(string operation, params object[] args)
        {
            int value = 0;
            switch (operation)
            {
                case "alpha": { value = 1; break; }
                case "charlie": { value = 2; break; }
                case "bravo": { value = 3; break; }
                case "delta": { value = 4; break; }
                case "echo": { value = 5; break; }
                case "foxtrot": { value = 6; break; }
                //case "golf": { value = 7; break; }
                //case "hotel": { value = 8; break; }
                //case "india": { value = 9; break; }
                default: { value = -1; break; }
            }

            return value;
        }
    }
}
