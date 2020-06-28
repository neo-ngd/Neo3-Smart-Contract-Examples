using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace StorageExample
{
    [Features(ContractFeatures.HasStorage)]
    public class Contract1 : SmartContract
    {
        public static event Action<string, uint> Saved;

        public static bool Put(string message)
        {
            if (Exists(message)) return false;
            var blockChainHeight = Blockchain.GetHeight();
            Storage.Put(message, blockChainHeight);
            Saved(message, blockChainHeight);

            return true;
        }

        public static int Get(string message)
        {
            if (!Exists(message)) return -1;
            return (int)Storage.Get(message).TryToBigInteger();
        }

        public static bool Exists(string message)
        {
            return Storage.Get(message) != null;
        }
    }

    public static class Helper
    {
        public static BigInteger TryToBigInteger(this byte[] value)
        {
            return value?.ToBigInteger() ?? 0;
        }
    }
}
