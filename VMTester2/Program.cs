using Neo;
using Neo.Cryptography;
using Neo.Cryptography.ECC;
using Neo.SmartContract;
using Neo.VM;
using Neo.Core;
//using Neo.SmartContract.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Neo.Implementations.Wallets.EntityFramework;

namespace VMTest2
{
    class Program
    {
        // This example was inspired by the following pages from the NEO Tutorial:
        // http://docs.neo.org/en-us/sc/tutorial/verify.html
        // http://docs.neo.org/en-us/sc/test.html
        // ...and @relfos' neo-debugger project: https://github.com/Relfos/neo-debugger-tools

        // Reference: https://github.com/mwherman2000/neo-windocs/blob/master/windocs/quickstart-csharp/09-deploytestsmartcontract.md#import-the-existing-developer-account-from-the-neo-privatenet-docker-container
        // Reference: https://hub.docker.com/r/metachris/neo-privnet-with-gas/#Wallet
        // ...as displayed by [account] > View Private Key in neo-gui-developer
        //public const string WIF2 = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr";
        //public const string WIF2Address = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        //public const string WIF2PublicKey = "031a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a";
        //public const string WIF2PrivateKeyHex = "1dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbb";
        //public static readonly byte[] WIF2AddressScriptHash = Neo.SmartContract.Framework.Helper.ToScriptHash(WIF2Address);

        static void Main(string[] args)
        {
            var engine = new ExecutionEngine(null, Crypto.Default);

            engine.LoadScript(File.ReadAllBytes(@"..\..\..\ReturnOperation1\bin\debug\ReturnOperation1.avm"));

            //
            // ReturnOperation1.avm
            //
            //public class ReturnOperation1 : SmartContract
            //{
            //    public static string Main(string operation, object[] args)
            //    {
            //        string message = operation + " world";
            //        Runtime.Log(message);
            //        Runtime.Notify(operation, message);
            //        return message;
            //    }
            //}

            // Dump out the loaded script...
            byte[] scriptReturnOperation1 = engine.CurrentContext.Script;
            Console.WriteLine("CurrentContext.Script:\t" + scriptReturnOperation1.Length);
            Console.WriteLine("CurrentContext.Script:\t" + BytesToHexString(scriptReturnOperation1).Length);
            Console.WriteLine("CurrentContext.Script:\t" + BytesToHexString(scriptReturnOperation1));
            Console.WriteLine("CurrentContext.Script:\t" + scriptReturnOperation1.ToHexString().Length);
            Console.WriteLine("CurrentContext.Script:\t" + scriptReturnOperation1.ToHexString());
            //Console.WriteLine("CurrentContext.Script:\t" + script.ToHexString().HexToBytes().Length);

            // Dump out the script's hash
            var scriptHash = engine.CurrentContext.ScriptHash;
            Console.WriteLine("CurrentContext.scriptHash:\t" + BytesToHexString(scriptHash).Length);
            Console.WriteLine("CurrentContext.scriptHash:\t" + BytesToHexString(scriptHash) + " wrong");
            var scriptHash2 = scriptReturnOperation1.ToScriptHash();
            Console.WriteLine("scriptReturnOperation1.ToScriptHash():\t" + scriptHash2.ToString().Length);
            Console.WriteLine("scriptReturnOperation1.ToScriptHash():\t" + scriptHash2.ToString() + " good");

            // Create an Contract Account and dump out it's properties (e.g. Contract Account address)
            // NOTE: You need to have th NEO privatenet configured and running. `contract.Address` needs config.json file. Checkout the following for details:
            //       https://github.com/mwherman2000/neo-windocs/blob/master/windocs/quickstart-csharp/07-installneoprivatenetcontainer.md
            //UInt160 accountPublicKeyHash = ((ECPoint)(object)WIF2PublicKey).EncodePoint(true).ToScriptHash();
            ContractParameterType[] scriptParameterListDeclaration = { ContractParameterType.String, ContractParameterType.Array };
            Contract contract = VerificationContract.Create(scriptParameterListDeclaration, scriptReturnOperation1);
            string contractAddress = contract.Address;
            string contractScript = contract.Script.ToHexString();
            string contractScriptHash = contract.ScriptHash.ToString();
            Console.WriteLine("contractAddress:\t" + contractAddress.Length.ToString() + " " + contractAddress);
            Console.WriteLine("contractScriptHash:\t" + contractScriptHash.Length.ToString() + " " + contractScriptHash);
            Console.WriteLine("contractScript:\t" + contractScript.Length.ToString() + " " + contractScript);

            //neo-gui hash:   0x431bfd28ecb875e807b8e8be365120adf63fb987
            //scriptHash:       87B93FF6AD205136BEE8B807E875B8EC28FD1B43  ReturnOperation1.avm
            //scriptHash2:    0x431bfd28ecb875e807b8e8be365120adf63fb987
            //neo-gui Address:  AU9WnnGD8AY3NqAqyZ98cF59X4SCk81aF9
            //contract.Address: AU9WnnGD8AY3NqAqyZ98cF59X4SCk81aF9

            string operationParameter = "hello";
            Console.WriteLine("operationParameter:\t" + operationParameter.ToString() + "\t" + operationParameter.ToString());

            ContractParameter integer1 = new ContractParameter(ContractParameterType.Integer);
            integer1.Value = 3;
            ContractParameter integer2 = new ContractParameter(ContractParameterType.Integer);
            integer2.Value = 4;
            ContractParameter[] arrayOfIntegers = { integer1, integer2 };
            ContractParameter arrayOfObjectParameter = new ContractParameter(ContractParameterType.Array);
            arrayOfObjectParameter.Value = arrayOfIntegers;
;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitPush(arrayOfObjectParameter);
                sb.EmitPush(operationParameter);
                engine.LoadScript(sb.ToArray());
            }

            bool singleStep = false; // Debugging the debugger
            if (singleStep)
            {
                engine.AddBreakPoint(0);
                engine.StepInto();
                engine.StepInto();
                engine.StepInto();
                engine.StepInto();
                engine.StepInto();
            }
            else
            {
                engine.Execute(); // start execution
            }

            Console.WriteLine("engine.State:\t{0}", engine.State.ToString());
            DumpExecutionContext(engine);
            DumpAltStack(engine);
            DumpEvaluationStack(engine);

            var result = engine.EvaluationStack.Peek().GetBigInteger(); // set the return value here
            Console.WriteLine($"Execution result {result}");
            var result2 = engine.EvaluationStack.Peek(); // set the return value here
            Console.WriteLine($"Execution result {result2}");
            var result3 = engine.EvaluationStack.Peek().GetString(); // set the return value here
            Console.WriteLine($"Execution result {result3}");
            Console.ReadLine();
        }

        private static void DumpAltStack(ExecutionEngine engine)
        {
            var stack = engine.AltStack;

            int offset = 0;
            Console.WriteLine("AlStack ----------");
            Console.WriteLine("AltStack.Count:\t{0}", stack.Count);
            foreach (var stackItem in stack)
            {
                Console.WriteLine("AltStack:\t{0}\t{1}", offset, stackItem.ToString());
                DumpStackItem(stackItem);
                offset++;
            }
        }

        private static void DumpEvaluationStack(ExecutionEngine engine)
        {
            Console.WriteLine("EvaluationStack ----------");
            //StackItem peekItem = engine.EvaluationStack.Peek(0);
            //Console.WriteLine("EvaluationStack.Peek():" + peekItem.ToString());
            //DumpStackItem(peekItem);

            var stack = engine.EvaluationStack;

            int offset = 0;
            Console.WriteLine("EvaluationStack.Count:\t{0}", stack.Count);
            foreach (var stackItem in stack)
            {
                Console.WriteLine("EvaluationStack:\t{0}\t{1}", offset, stackItem.ToString());
                DumpStackItem(stackItem);
                offset++;
            }
        }

        private static void DumpStackItem(StackItem stackItem)
        {
            var typeStackItem = stackItem.GetType();
            switch (typeStackItem.ToString())
            {
                case "Neo.VM.Types.Integer":
                    {
                        Neo.VM.Types.Integer value = (Neo.VM.Types.Integer)stackItem;
                        BigInteger bigvalue = value.GetBigInteger();
                        Console.WriteLine("Neo.VM.Types.Integer:\t{0}", bigvalue.ToString());
                        break;
                    }
                case "Neo.VM.Types.Array":
                    {
                        Neo.VM.Types.Array items = (Neo.VM.Types.Array)stackItem;
                        int offset = 0;
                        Console.WriteLine("Array.Count:\t{0}", items.Count);
                        foreach (var item in items)
                        {
                            Console.WriteLine("Neo.VM.Types.Array:\t{0}\t{1}", offset, item.ToString());
                            var typeItem = item.GetType();
                            switch (typeItem.ToString())
                            {
                                case "Neo.VM.Types.Boolean":
                                    {
                                        int offset2 = 0;
                                        Neo.VM.Types.Boolean b = (Neo.VM.Types.Boolean)item;
                                        Console.WriteLine("    Neo.VM.Types.Boolean:\t{0}\t{1}", offset2, b.GetBoolean());
                                        break;
                                    }
                                case "Neo.VM.Types.Integer":
                                    {
                                        int offset2 = 0;
                                        Neo.VM.Types.Integer i = (Neo.VM.Types.Integer)item;
                                        Console.WriteLine("    Neo.VM.Types.Integer:\t{0}\t{1}\t{2}", offset2, i.GetBigInteger(), i.GetBigInteger().ToString("X"));
                                        break;
                                    }
                                case "Neo.VM.Types.Array":
                                    {
                                        var items2 = ((Neo.VM.Types.Array)item);
                                        int offset2 = 0;
                                        foreach (var item2 in items2)
                                        {
                                            Console.WriteLine("    Neo.VM.Types.Array:\t{0}\t{1}\t{2}", offset2, item2.GetBigInteger(), item2.GetBigInteger());
                                            offset2++;
                                        }
                                        break;
                                    }
                                case "Neo.VM.Types.ByteArray":
                                    {
                                        byte[] items2 = ((Neo.VM.Types.ByteArray)item).GetByteArray();
                                        int offset2 = 0;
                                        foreach (var item2 in items2)
                                        {
                                            Console.WriteLine("    Neo.VM.Types.ByteArray:\t{0}\t{1}\t{2}\t{3}", offset2, item2.ToString(), item2.ToString("X"), Convert.ToChar(item2).ToString());
                                            offset2++;
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        throw new NotImplementedException(typeItem.ToString() + " unknown");
                                    }
                            }
                            offset++;
                        }
                        break;
                    }
                case "Neo.VM.Types.ByteArray":
                    {
                        byte[] items = ((Neo.VM.Types.ByteArray)stackItem).GetByteArray();
                        int offset = 0;
                        foreach (var item in items)
                        {
                            Console.WriteLine("Neo.VM.Types.ByteArray:\t{0}\t{1}\t{2}\t{3}", offset, item.ToString(), item.ToString("X"), Convert.ToChar(item).ToString());
                            offset++;
                        }
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException(typeStackItem.ToString() + " unknown");
                    }
            }
        }

        private static void DumpExecutionContext(ExecutionEngine engine)
        {
            Console.WriteLine("CurrentContext ----------");
            using (var ctx = engine.CurrentContext)
            {
                Console.WriteLine("State:\t" + engine.State.ToString());
                Console.WriteLine("InstructionPointer:\t" + ctx.InstructionPointer.ToString());
                Console.WriteLine("NextInstruction:\t" + ctx.NextInstruction.ToString());
                var script = ctx.Script.ToArray();
                int offset = 0;
                Neo.VM.OpCode eOpCode;
                string eOpCodeName;
                foreach (var opcode in script)
                {
                    eOpCode = (Neo.VM.OpCode)Enum.ToObject(typeof(Neo.VM.OpCode), opcode);
                    eOpCodeName = eOpCode.ToString();
                    string tag = "";
                    if (offset == ctx.InstructionPointer) tag = "<<< NEXT";
                    Console.WriteLine("opcode:\t{0}\t{1}\t{2}\t{3,-16}\t{4,-16}\t{5}", offset, opcode, opcode.ToString("X"), eOpCode.ToString(), eOpCodeName, tag);
                    offset++;
                }
            }
        }

        // Reference: https://stackoverflow.com/questions/623104/byte-to-hex-string
        private static string BytesToHexString(byte[] bytes)
        {
            string s = "";
            if (bytes.Length > 0) s = BitConverter.ToString(bytes).Replace("-", string.Empty);
            return s;
        }
    }
}
