using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace Accounts1
{
    public class Accounts1 : SmartContract
    {
        public const string WIF1 = "L3f7C21q4Mu5FzZsDuCMeHqwJ1apHYCrwzU2821p1opaM43BAMKo";
        public const string WIF1Address = "AcCHoikUq9cP6SMESHufCEMwADJNcTwnAv";
        public const string WIF1PublicKey = "02c44534465c8b21f659eba5708e69edae1ddd6f8cd63004095f8e39493cf54e82";
        public const string WIF1PrivateKeyHex = "c016e1c8a193cc1a28a15464106b91b52727547a3a36f40a8bfebd9933d1963c";
        public static readonly byte[] WIF1AccountScriptHash = WIF1Address.ToScriptHash();

        public const string WIF2 = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr";
        public const string WIF2Address = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y";
        public const string WIF2PublicKey = "031a6c6fbbdf02ca351745fa86b9ba5a9452d785ac4f7fc2b7548ca2a46c4fcf4a";
        public const string WIF2PrivateKeyHex = "1dd37fba80fec4e6a6f13fd708d8dcb3b29def768017052f6c930fa1c5d90bbb";
        public static readonly byte[] WIF2AccountScriptHash = WIF2Address.ToScriptHash();

        public static void Main()
        {
            Runtime.Notify("WIF1", WIF1);
            Runtime.Notify("WIF1Address", WIF1Address);
            Runtime.Notify("WIF1PublicKey", WIF1PublicKey);
            Runtime.Notify("WIF1PrivateKeyHex", WIF1PrivateKeyHex);
            Runtime.Notify("WIF1AccountScriptHash", WIF1AccountScriptHash);
            Runtime.Notify("WIF2", WIF2);
            Runtime.Notify("WIF2Address", WIF2Address);
            Runtime.Notify("WIF2PublicKey", WIF2PublicKey);
            Runtime.Notify("WIF2PrivateKeyHex", WIF2PrivateKeyHex);
            Runtime.Notify("WIF2AccountScriptHash", WIF2AccountScriptHash);
        }
    }
}
