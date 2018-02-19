using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

// System.Exception: error:System.Void StatementsSwitch.StatementsSwitch::Main(System.Int32)::IL_0040 Switch  ---> System.Exception: need neo.VM update.


namespace StatementsSwitch
{
    public class ContractStatementsSwitchInt : SmartContract
    {
        public static void Main(int value)
        {
            int caseValue1 = 0;
            switch (value)
            {
                case 1: { caseValue1 = 1; break; }
                default: { caseValue1 = -1; break; }
            }

            int caseValue2 = 0;
            switch (value)
            {
                case 1: { caseValue2 = 1; break; }
                case 2: { caseValue2 = 2; break; }
                default: { caseValue2 = -1; break; }
            }

            //int caseValue3 = 0;
            //switch (value)
            //{
            //    case 1: { caseValue3 = 1; break; }
            //    case 2: { caseValue3 = 2; break; }
            //    case 3: { caseValue3 = 3; break; }
            //    default: { caseValue3 = -1; break; }
            //}

            //int caseValue4 = 0;
            //switch (value)
            //{
            //    case 1: { caseValue4 = 1; break; }
            //    case 2: { caseValue4 = 2; break; }
            //    case 3: { caseValue4 = 3; break; }
            //    case 4: { caseValue4 = 4; break; }
            //    default: { caseValue4 = -1; break; }
            //}

            //int caseValue5 = 0;
            //switch (value)
            //{
            //    case 1: { caseValue5 = 1; break; }
            //    case 2: { caseValue5 = 2; break; }
            //    case 3: { caseValue5 = 3; break; }
            //    case 4: { caseValue5 = 4; break; }
            //    case 5: { caseValue5 = 5; break; }
            //    default: { caseValue5 = -1; break; }
            //}

            //int caseValue6 = 0;
            //switch (value)
            //{
            //    case 1: { caseValue6 = 1; break; }
            //    case 2: { caseValue6 = 2; break; }
            //    case 3: { caseValue6 = 3; break; }
            //    case 4: { caseValue6 = 4; break; }
            //    case 5: { caseValue6 = 5; break; }
            //    case 6: { caseValue6 = 6; break; }
            //    default: { caseValue6 = -1; break; }
            //}

            //int caseValue7 = 0;
            //switch (value)
            //{
            //    case 1: { caseValue7 = 1; break; }
            //    case 2: { caseValue7 = 2; break; }
            //    case 3: { caseValue7 = 3; break; }
            //    case 4: { caseValue7 = 4; break; }
            //    case 5: { caseValue7 = 5; break; }
            //    case 6: { caseValue7 = 6; break; }
            //    case 7: { caseValue7 = 7; break; }
            //    default: { caseValue7 = -1; break; }
            //}

            Storage.Put(Storage.CurrentContext, "Hello", "World");
        }
    }
}
