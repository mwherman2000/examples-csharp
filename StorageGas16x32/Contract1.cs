
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace StorageGas16x32
{
    public class Contract1 : SmartContract
    {
        public static bool Main()
        {
            byte[] bytes32 = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
                               0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,};

            Storage.Put(Storage.CurrentContext, new byte[] { 0x00 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x01 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x02 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x03 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x04 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x05 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x06 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x07 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x08 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x09 }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x0a }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x0b }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x0c }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x0d }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x0e }, bytes32);
            Storage.Put(Storage.CurrentContext, new byte[] { 0x0f }, bytes32);

            return true;
        }
    }
}
