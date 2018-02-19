using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace BlockTxNumbers1
{
    public class Contract1 : SmartContract
    {
        public static void Main()
        {
            Runtime.Log("========================================");
            var esh = ExecutionEngine.ExecutingScriptHash;
            Runtime.Notify("ExecutionEngine.ExecutingScriptHash", esh);

            var h = Blockchain.GetHeight();
            Runtime.Notify("Blockchain.GetHeight()$UIN", h);

            Header hdr = Blockchain.GetHeader(h);
            Runtime.Notify("hdr.Timestamp$UIN", hdr.Timestamp);
            Runtime.Notify("hdr.Timestamp$TIM", hdr.Timestamp);
            Runtime.Notify("hdr.PrevHash", hdr.PrevHash);
            Runtime.Notify("hdr.NextConsensus", hdr.NextConsensus);
            //Runtime.Notify("hdr.Index$UIN", hdr.Index);
            Runtime.Notify("hdr.ConsensusData$UIN", hdr.ConsensusData);

            Block b = Blockchain.GetBlock(h);
            Runtime.Notify("b.GetTransactionCount()$INT", b.GetTransactionCount());
            Runtime.Notify("b.Hash", b.Hash);
            //Runtime.Notify("b.Index$INT", b.Index);
            Runtime.Notify("b.Timestamp$TIM", b.Timestamp);

            Transaction[] txs = b.GetTransactions();
            int offset = 0;
            foreach(Transaction t in txs)
            {
                Runtime.Notify("offset$INT", offset);
                Runtime.Notify("t.Hash", t.Hash);
                Runtime.Notify("t.Type$INT", t.Type);
                offset++;
            }

            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionInput[] inputs = tx.GetInputs();
            Runtime.Notify("tx.GetInputs()", inputs.Length);
            foreach (TransactionInput i in inputs)
            {
                Runtime.Notify("GetInputs: PrevHash", i.PrevHash);
                Runtime.Notify("GetInputs: PrevIndex", i.PrevIndex);
            }
        }
    }
}
