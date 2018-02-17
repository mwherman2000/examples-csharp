using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace Transactions2
{
    public class Transactions2 : SmartContract
    {
        public const string WIF1 = "L3f7C21q4Mu5FzZsDuCMeHqwJ1apHYCrwzU2821p1opaM43BAMKo";
        public const string WIF1Address = "AcCHoikUq9cP6SMESHufCEMwADJNcTwnAv";
        public const string WIF1PublicKey = "02c44534465c8b21f659eba5708e69edae1ddd6f8cd63004095f8e39493cf54e82";
        public const string WIF1PrivateKeyHex = "c016e1c8a193cc1a28a15464106b91b52727547a3a36f40a8bfebd9933d1963c";
        public static readonly byte[] WIF1AddressScriptHash = WIF1Address.ToScriptHash();

        public const string WIF2 = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr";
        public const string WIF2Address = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        public const string WIF2PublicKey = "031a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a";
        public const string WIF2PrivateKeyHex = "1dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbb";
        public static readonly byte[] WIF2AddressScriptHash = WIF2Address.ToScriptHash();

        public static void Main()
        {
            Runtime.Log("WIF1...");
            Runtime.Notify("WIF1", WIF1.Length, WIF1);
            Runtime.Notify("WIF1Address", WIF1Address.Length, WIF1Address);
            Runtime.Notify("WIF1PublicKey", WIF1PublicKey.Length, WIF1PublicKey);
            Runtime.Notify("WIF1PrivateKeyHex", WIF1PrivateKeyHex.Length, WIF1PrivateKeyHex);
            Runtime.Notify("WIF1AccountScriptHash", WIF1AddressScriptHash.Length, WIF1AddressScriptHash);

            Runtime.Log("WIF2...");
            Runtime.Notify("WIF2", WIF2.Length, WIF2);
            Runtime.Notify("WIF2Address", WIF2Address.Length, WIF2Address);
            Runtime.Notify("WIF2PublicKey", WIF2PublicKey.Length, WIF2PublicKey);
            Runtime.Notify("WIF2PrivateKeyHex", WIF2PrivateKeyHex.Length, WIF2PrivateKeyHex);
            Runtime.Notify("WIF2AccountScriptHash", WIF2AddressScriptHash.Length, WIF2AddressScriptHash);

            //byte[] csh = ExecutionEngine.CallingScriptHash;
            //if (csh.Length> 0 ) Runtime.Notify("CallingScriptHash:", csh.Length, csh);
            byte[] ensh = ExecutionEngine.EntryScriptHash;
            if (ensh.Length > 0) Runtime.Notify("EntryScriptHash: ", ensh.Length, ensh);
            byte[] exsh = ExecutionEngine.ExecutingScriptHash;
            if (exsh.Length > 0) Runtime.Notify("ExecutionEngine:", exsh.Length, exsh);

            Runtime.Log("GetSenderScriptHash...");
            byte[] ssh = GetSenderScriptHash();
            if (ssh.Length == 0)
            {
                Runtime.Notify("GetSenderScriptHash: senderSH", 0xbadbad);
            }
            else
            {
                Runtime.Notify("GetSenderScriptHash: senderSH", ssh.Length, ssh);
            }

            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;

            //Runtime.Log("tx.GetAttributes...");
            //TransactionAttribute[] attrs = tx.GetAttributes();
            //foreach (TransactionAttribute a in attrs)
            //{
            //    Runtime.Notify("GetAttributes: Data,Usage", a.Data, a.Usage);
            //}

            Runtime.Log("tx.GetInputs()...");
            TransactionInput[] inputs = tx.GetInputs();
            Runtime.Notify("GetInputs:" , inputs.Length);
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
                Runtime.Notify("GetOutputs: Value", o.Value);
            }

            Runtime.Log("tx.GetReferences()...");
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetReferences:", refs.Length);
            foreach (TransactionOutput r in refs)
            {
                Runtime.Notify("GetReferences: AssetId", r.AssetId.Length, r.AssetId);
                Runtime.Notify("GetReferences: ScriptHash", r.ScriptHash.Length, r.ScriptHash);
                Runtime.Notify("GetReferences: Value", r.Value);
            }

            //Runtime.Log("tx.GetUnspentCoins...");
            //TransactionOutput[] unspents = tx.GetUnspentCoins();
            //Runtime.Notify("GetUnspentCoins:", unspents.Length);
            //foreach (TransactionOutput c in unspents)
            //{
            //    Runtime.Notify("GetUnspentCoins: AssetId,ScriptHash,Value:", c.AssetId, c.ScriptHash, c.Value);
            //}
        }

        private static readonly byte[] NEO_ASSET_ID = { 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
        private static byte[] GetSenderScriptHash()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetSenderScriptHash:", refs.Length);
            Runtime.Notify("GetSenderScriptHash: NEO_ASSET_ID", NEO_ASSET_ID, NEO_ASSET_ID.Length);
            foreach (TransactionOutput r in refs)
            {
                Runtime.Notify("GetSenderScriptHash: AssetId", r.AssetId.Length, r.AssetId);
                if (r.AssetId == NEO_ASSET_ID)
                {
                    Runtime.Notify("GetSenderScriptHash: AssetId,ScriptHash", r.AssetId.Length, r.AssetId, r.ScriptHash.Length, r.ScriptHash);
                    return r.ScriptHash;
                }
            }
            return new byte[] { };
        }
    }
}
