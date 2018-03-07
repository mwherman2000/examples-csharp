using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace Transactions2
{
    public class Transactions2 : SmartContract
    {
        // WIF from the NEO privatenet Python environment
        public const string WIF2                     = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr";
        public const string WIF2AccountAddress       = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        public const string WIF2AccountPublicKey     = "031a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a";
        public const string WIF2AccountPrivateKeyHex = "1dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbb";
        public static readonly byte[] WIF2AccountAddressScriptHash = WIF2AccountAddress.ToScriptHash();

        public const string WIF3                     = "L4SVwFWyzHQonAyBcCMqJuSHz8RuBqieWxxmxrrXUrsQ4hkJYpSC";
        public const string WIF3AccountAddress       = "AeD4zRqCoFnZ2AFHr5UseiJEZ9ySxTAWG2";
        public const string WIF3AccountPublicKey     = "037126adc0c5637faff57ff1dfc4fa0b2134ab4bb2135681779a0c08202301b003";
        public const string WIF3AccountPrivateKeyHex = "d770da3a76e8314bd810a8d0af69ecb771915efd1674330b3bb5beb79917a472";
        public static readonly byte[] WIF3AccountAddressScriptHash = WIF3AccountAddress.ToScriptHash();

        public const string SampleScriptHash = "0x2a846575ffad543ac226aaca17905c48aee38594";
        public static readonly byte[] SampleScriptHashAsByteArray = "0x2a846575ffad543ac226aaca17905c48aee38594".AsByteArray();

        public static bool Main()
        {
            Runtime.Notify("WIF2Account (from privatenet WIF...");
            Runtime.Notify("WIF2", /* WIF2.Length, */ WIF2);
            Runtime.Notify("WIF2AccountAddress", /* WIF2AccountAddress.Length, */ WIF2AccountAddress);
            Runtime.Notify("WIF2AccountPublicKey", /* WIF2AccountPublicKey.Length, */ WIF2AccountPublicKey);
            Runtime.Notify("WIF2AccountPrivateKeyHex", /* WIF2AccountPrivateKeyHex.Length, */ WIF2AccountPrivateKeyHex);
            Runtime.Notify("WIF2AccountScriptHash", /* WIF2AccountAddressScriptHash.Length, */ WIF2AccountAddressScriptHash);
            Runtime.Notify("WIF2AccountScriptHash$BIN", /* WIF2AccountAddressScriptHash.Length, */ WIF2AccountAddressScriptHash);
            Runtime.Notify("WIF2AccountScriptHash$STR", /* WIF2AccountAddressScriptHash.Length, */ WIF2AccountAddressScriptHash);
            Runtime.Notify("WIF2AccountScriptHash$160", /* WIF2AccountAddressScriptHash.Length, */ WIF2AccountAddressScriptHash);

            Runtime.Notify("WIF3Account (transient - new with each new neo-gui wallet...");
            Runtime.Notify("WIF3", /* WIF3.Length, */ WIF3);
            Runtime.Notify("WIF3AccountAddress", /* WIF3AccountAddress.Length, */ WIF3AccountAddress);
            Runtime.Notify("WIF3AccountPublicKey", /* WIF3AccountPublicKey.Length, */ WIF3AccountPublicKey);
            Runtime.Notify("WIF3AccountPrivateKeyHex", /* WIF3AccountPrivateKeyHex.Length, */ WIF3AccountPrivateKeyHex);
            Runtime.Notify("WIF3AccountScriptHash", /* WIF3AccountAddressScriptHash.Length, */ WIF3AccountAddressScriptHash);
            Runtime.Notify("WIF3AccountScriptHash$BIN", /* WIF3AccountAddressScriptHash.Length, */ WIF3AccountAddressScriptHash);
            Runtime.Notify("WIF3AccountScriptHash$STR", /* WIF3AccountAddressScriptHash.Length, */ WIF3AccountAddressScriptHash);
            Runtime.Notify("WIF3AccountScriptHash$160", /* WIF3AccountAddressScriptHash.Length, */ WIF3AccountAddressScriptHash);

            Runtime.Notify("ExecutionEngine tests...");
            //byte[] csh = ExecutionEngine.CallingScriptHash;
            //if (csh.Length> 0 ) Runtime.Notify("CallingScriptHash:", csh.Length, csh);
            byte[] ensh = ExecutionEngine.EntryScriptHash;
            if (ensh.Length > 0)
            {
                Runtime.Notify("ExecutionEngine.EntryScriptHash: ", /* ensh.Length, */ ensh);
                Runtime.Notify("ExecutionEngine.EntryScriptHash$STR: ", /* ensh.Length, */ ensh);
            }
            byte[] exsh = ExecutionEngine.ExecutingScriptHash;
            if (exsh.Length > 0)
            {
                Runtime.Notify("ExecutionEngine.ExecutingScriptHash:", /* exsh.Length, */ exsh);
                Runtime.Notify("ExecutionEngine.ExecutingScriptHash$STR:", /* exsh.Length, */ exsh);
            }

            Runtime.Notify("SampleScriptHash:", SampleScriptHash);
            Runtime.Notify("SampleScriptHash$STR:", SampleScriptHash);
            Runtime.Notify("SampleScriptHashAsByteArray", SampleScriptHashAsByteArray);
            Runtime.Notify("SampleScriptHashAsByteArray$STR", SampleScriptHashAsByteArray);

            //Runtime.Notify("Some simple tests...");
            //Runtime.Notify("0xbadbad", 0xbadbad);
            //Runtime.Notify("(Boolean)true", (Boolean)true);
            //Runtime.Notify("(Boolean)false", (Boolean)false);
            //Runtime.Notify("Value$INT 16", 16);
            //Runtime.Notify("Value BI 16", (BigInteger)16);
            //Runtime.Notify("Value$neo 16", 16);
            //Runtime.Notify("Value$neo BI 16", (BigInteger)16);
            //Runtime.Notify("Value$INT 123456789", 123456789);
            //Runtime.Notify("Value BI 123456789", (BigInteger)123456789);
            //Runtime.Notify("Value$neo 123456789", 123456789);
            //Runtime.Notify("Value$NEO BI 1234", (BigInteger)1234);
            //Runtime.Notify("Value$NEO BI 123456789123", (BigInteger)123456789123);
            //Runtime.Notify("Value$gas BI 123456789", (BigInteger)123456789);

            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            Runtime.Notify("ExecutionEngine tests: tx.GetInputs()...");
            TransactionInput[] inputs = tx.GetInputs();
            Runtime.Notify("GetInputs:" , inputs.Length);
            foreach (TransactionInput i in inputs)
            {
                Runtime.Notify("GetInputs: PrevHash", /* i.PrevHash.Length, */ i.PrevHash);
                Runtime.Notify("GetInputs: PrevIndex", i.PrevIndex);
            }

            Runtime.Notify("ExecutionEngine tests: tx.GetOutputs()...");
            TransactionOutput[] outputs = tx.GetOutputs();
            Runtime.Notify("GetOutputs:", outputs.Length);
            foreach (TransactionOutput o in outputs)
            {
                Runtime.Notify("GetOutputs: AssetId", /* o.AssetId.Length, */ o.AssetId);
                Runtime.Notify("GetOutputs: ScriptHash", /* o.ScriptHash.Length, */ o.ScriptHash);
                Runtime.Notify("GetOutputs: Value$NEO", o.Value);
            }

            Runtime.Notify("ExecutionEngine tests: tx.GetReferences()...");
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetReferences:", refs.Length);
            foreach (TransactionOutput r in refs)
            {
                Runtime.Notify("GetReferences: AssetId", /* r.AssetId.Length, */ r.AssetId);
                Runtime.Notify("GetReferences: ScriptHash", /* r.ScriptHash.Length, */ r.ScriptHash);
                Runtime.Notify("GetReferences: Value$NEO", r.Value);
            }

            //Runtime.Notify("tx.GetUnspentCoins...");
            //TransactionOutput[] unspents = tx.GetUnspentCoins();
            //Runtime.Notify("GetUnspentCoins:", unspents.Length);
            //foreach (TransactionOutput c in unspents)
            //{
            //    Runtime.Notify("GetUnspentCoins: AssetId,ScriptHash,Value:", c.AssetId, c.ScriptHash, c.Value);
            //}

            return true;
        }

        private static readonly byte[] NEO_ASSET_ID = { 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
        private static byte[] GetSenderScriptHash()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetSenderScriptHash:", refs.Length);
            Runtime.Notify("GetSenderScriptHash: NEO_ASSET_ID", NEO_ASSET_ID);
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
