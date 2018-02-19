using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

// Reference: http://docs.neo.org/en-us/sc/tutorial/verify.html

namespace ContractAccount1
{
    public class Contract1 : SmartContract
    {
        public static bool Main(byte[] signature)
        {
            Runtime.Notify("Main", signature);

            byte[] ensh = ExecutionEngine.EntryScriptHash;
            if (ensh.Length > 0) Runtime.Notify("EntryScriptHash: ", ensh.Length, ensh);
            byte[] exsh = ExecutionEngine.ExecutingScriptHash;
            if (exsh.Length > 0) Runtime.Notify("ExecutionEngine:", exsh.Length, exsh);

            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;

            //Runtime.Log("tx.GetAttributes...");
            //TransactionAttribute[] attrs = tx.GetAttributes();
            //foreach (TransactionAttribute a in attrs)
            //{
            //    Runtime.Notify("GetAttributes: Data,Usage", a.Data, a.Usage);
            //}

            Runtime.Log("tx.GetInputs()...");
            TransactionInput[] inputs = tx.GetInputs();
            Runtime.Notify("GetInputs:", inputs.Length);
            foreach (TransactionInput i in inputs)
            {
                Runtime.Notify("GetInputs: PrevHash", i.PrevHash.Length, i.PrevHash);
                Runtime.Notify("GetInputs: PrevIndex", i.PrevIndex);
            }

            Runtime.Log("tx.GetOutputs()...");
            TransactionOutput[] outputs = tx.GetOutputs();
            Runtime.Notify("GetOutputs:", outputs.Length);
            foreach (TransactionOutput o in outputs)
            {
                Runtime.Notify("GetOutputs: AssetId", o.AssetId.Length, o.AssetId);
                Runtime.Notify("GetOutputs: ScriptHash", o.ScriptHash.Length, o.ScriptHash);
                Runtime.Notify("GetOutputs: Value$NEO", o.Value);
            }

            Runtime.Log("tx.GetReferences()...");
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetReferences:", refs.Length);
            foreach (TransactionOutput r in refs)
            {
                Runtime.Notify("GetReferences: AssetId", r.AssetId.Length, r.AssetId);
                Runtime.Notify("GetReferences: ScriptHash", r.ScriptHash.Length, r.ScriptHash);
                Runtime.Notify("GetReferences: Value$NEO", r.Value);
            }

            //Runtime.Log("tx.GetUnspentCoins...");
            //TransactionOutput[] unspents = tx.GetUnspentCoins();
            //Runtime.Notify("GetUnspentCoins:", unspents.Length);
            //foreach (TransactionOutput c in unspents)
            //{
            //    Runtime.Notify("GetUnspentCoins: AssetId,ScriptHash,Value:", c.AssetId, c.ScriptHash, c.Value);
            //}

            return true;
        }
    }
}
