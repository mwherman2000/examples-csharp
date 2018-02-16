using Neo.Cryptography;
using Neo.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldUnitTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new ExecutionEngine(null, Crypto.Default);
            engine.LoadScript(File.ReadAllBytes(@"..\..\..\Transactions1Small\bin\debug\Transactions1Small.avm"));

            int value = 1234;
            Console.WriteLine("value:\t" + value.ToString() + "\t" + value.ToString("X"));

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitPush(value); // corresponds to the parameter value
                engine.LoadScript(sb.ToArray());
            }

            bool singleStep = false;
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

            DumpExecutionContext(engine);
            DumpAltStack(engine);
            DumpEvaluationStack(engine);

            var result = engine.EvaluationStack.Peek().GetBigInteger(); // set the return value here
            Console.WriteLine($"Execution result {result}");
            Console.ReadLine();
        }

        private static void DumpAltStack(ExecutionEngine engine)
        {
            var stack = engine.AltStack;

            int offset = 0;
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
                                        Neo.VM.Types.Boolean b = (Neo.VM.Types.Boolean)item;
                                        Console.WriteLine("    Neo.VM.Types.Boolean:\t{0}\t{1}", offset, b.GetBoolean());
                                        break;
                                    }
                                case "Neo.VM.Types.Integer":
                                    {
                                        Neo.VM.Types.Integer i = (Neo.VM.Types.Integer)item;
                                        Console.WriteLine("    Neo.VM.Types.Integer:\t{0}\t{1}\t{2}", offset, i.GetBigInteger(), i.GetBigInteger().ToString("X"));
                                        break;
                                    }
                                case "Neo.VM.Types.ByteArray":
                                    {
                                        byte[] items2 = ((Neo.VM.Types.ByteArray)item).GetByteArray();
                                        int offset2 = 0;
                                        foreach (var item2 in items2)
                                        {
                                            Console.WriteLine("    Neo.VM.Types.ByteArray:\t{0}\t{1}\t{2}\t{3}", offset, item2.ToString(), item2.ToString("X"), Convert.ToChar(item2).ToString());
                                            offset++;
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
                            Console.WriteLine("Neo.VM.Types.ByteArray:\t{0}\t{1}\t{2}", offset, item.ToString(), item.ToString("X"));
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
            using (var ctx = engine.CurrentContext)
            {
                Console.WriteLine("State:\t" + engine.State.ToString());
                Console.WriteLine("InstructionPointer:\t" + ctx.InstructionPointer.ToString());
                Console.WriteLine("NextInstruction:\t" + ctx.NextInstruction.ToString());
                var script = ctx.Script.ToArray();
                int offset = 0;
                OpCode eOpCode;
                string eOpCodeName;
                foreach (var opcode in script)
                {
                    eOpCode = (OpCode)Enum.ToObject(typeof(OpCode), opcode);
                    eOpCodeName = eOpCode.ToString();
                    string tag = "";
                    if (offset == ctx.InstructionPointer) tag = "<<< NEXT";
                    Console.WriteLine("opcode:\t{0}\t{1}\t{2}\t{3,-16}\t{4,-16}\t{5}", offset, opcode, opcode.ToString("X"), eOpCode.ToString(), eOpCodeName, tag);
                    offset++;
                }
            }
        }
    }
}
