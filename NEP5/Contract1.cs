using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace NEP5
{
    [ManifestExtra("Author", "Chen, Zhitong")]
    [ManifestExtra("Email", "chenzhitong@onchain.com")]
    [ManifestExtra("Description", "This is a NEP5 example")]
    [Features(ContractFeatures.HasStorage)]
    public class NEP5 : SmartContract
    {
        [DisplayName("Transfer")]
        public static event Action<byte[], byte[], BigInteger> OnTransfer;

        private static readonly BigInteger TotalSupplyValue = 10000000000000000;

        private static readonly byte[] Owner = "NfKA6zAixybBHHpmaPYPDywoqDaKzfMPf9".ToScriptHash(); //Owner Address

        public static bool Deploy()
        {
            if (TotalSupply() != 0) return false;

            TotalSupplyStorage.Increase(TotalSupplyValue);
            AssetStorage.Increase(Owner, TotalSupplyValue);

            OnTransfer(null, Owner, TotalSupplyValue);
            return true;
        }

        public static BigInteger BalanceOf(byte[] account) => AssetStorage.Get(account);

        public static byte Decimals() => 8;

        public static string Name() => "MyToken"; //name of the token

        public static string Symbol() => "MYT"; //symbol of the token

        public static string[] SupportedStandards() => new string[] { "NEP-5", "NEP-7", "NEP-10" };

        public static BigInteger TotalSupply() => TotalSupplyStorage.Get();

        public static bool Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            if (!ValidateAddress(from) || !ValidateAddress(to)) throw new Exception("The parameters from and to SHOULD be 20-byte addresses.");
            if (amount <= 0) throw new Exception("The parameter amount MUST be greater than 0.");
            if (!IsPayable(to)) throw new Exception("Receiver cannot receive.");
            if (!Runtime.CheckWitness(from) && !from.Equals(ExecutionEngine.CallingScriptHash)) throw new Exception("No authorization.");
            if (AssetStorage.Get(from) < amount) throw new Exception("Insufficient balance.");
            if (amount == 0 || from == to) return true;

            AssetStorage.Reduce(from, amount);
            AssetStorage.Increase(to, amount);

            OnTransfer(from, to, amount);
            return true;
        }

        public static bool Update(byte[] script, string manifest)
        {
            if (!IsOwner()) return false;
            Contract.Update(script, manifest);
            return true;
        }

        private static bool IsOwner() => Runtime.CheckWitness(Owner);

        private static bool IsPayable(byte[] to) => Blockchain.GetContract(to)?.IsPayable ?? true;

        private static bool ValidateAddress(byte[] address) => address.Length == 20 && address.TryToBigInteger() != 0;
    }

    public static class Helper
    {
        public static BigInteger TryToBigInteger(this byte[] value) => value?.ToBigInteger() ?? 0;
    }
}