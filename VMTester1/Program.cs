using Neo.VM;
using Neo.Cryptography;
using System.IO;
using System;

// Reference: https://github.com/neo-project/docs/blob/master/en-us/sc/test.md

namespace ConsoleApplication1
{
    class program
    {
        static void Main(string[] args)
        {
            var engine = new ExecutionEngine(null, Crypto.Default);
            engine.LoadScript(File.ReadAllBytes(@"..\..\..\Transactions1Small\bin\debug\Transactions1Small.avm"));

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                //sb.EmitPush(2); // corresponds to the parameter c
                //sb.EmitPush(4); // corresponds to the parameter b
                //sb.EmitPush(3); // corresponds to the parameter a
                engine.LoadScript(sb.ToArray());
            }

            engine.Execute(); // start execution

            var result = engine.EvaluationStack.Peek().GetBigInteger(); // set the return value here
            Console.WriteLine($"Execution result {result}");
            Console.ReadLine();
        }
    }
}