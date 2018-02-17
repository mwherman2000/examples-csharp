using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://programmingblockchain.gitbooks.io/programmingblockchain/content/bitcoin_transfer/private_key.html

namespace BitcoinSecret1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Private key to WIF
            //Key privateKey = new Key(); // generate a random private key
            Key privateKey = new Key();
            BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);  // generate our Bitcoin secret(also known as Wallet Import Format or simply WIF) from our private key for the mainnet
            BitcoinSecret testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);  // generate our Bitcoin secret(also known as Wallet Import Format or simply WIF) from our private key for the testnet
            Console.WriteLine(mainNetPrivateKey); // L5B67zvrndS5c71EjkrTJZ99UaoVbMUAK58GKdQUfYCpAa6jypvn
            Console.WriteLine(testNetPrivateKey); // cVY5auviDh8LmYUW8AfafseD6p6uFoZrP7GjS3rzAerpRKE9Wmuz
            bool WifIsBitcoinSecret = mainNetPrivateKey == privateKey.GetWif(Network.Main);
            Console.WriteLine(WifIsBitcoinSecret); // True

            //Key privateKey = new Key(); // generate a random private key
            BitcoinSecret bitcoinSecret = privateKey.GetWif(Network.Main); // L5B67zvrndS5c71EjkrTJZ99UaoVbMUAK58GKdQUfYCpAa6jypvn
            Key samePrivateKey = bitcoinSecret.PrivateKey;
            Console.WriteLine(bitcoinSecret.PrivateKey);
            Console.WriteLine(bitcoinSecret.PubKey);
            Console.WriteLine(bitcoinSecret.PubKeyHash);
            Console.WriteLine(bitcoinSecret.ScriptPubKey);
            Console.WriteLine(samePrivateKey == privateKey); // True

            PubKey publicKey = privateKey.PubKey;
            BitcoinPubKeyAddress bitcoinPublicKey = publicKey.GetAddress(Network.Main); // 1PUYsjwfNmX64wS368ZR5FMouTtUmvtmTY
            //PubKey samePublicKey = bitcoinPublicKey.ItIsNotPossible;

            Console.ReadLine();
        }
    }
}
