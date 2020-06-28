using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Native_
{
    [ManifestExtra("Author", "Chen, Zhitong")]
    [ManifestExtra("Email", "chenzhitong@onchain.com")]
    [ManifestExtra("Description", "This is a example")]
    public class Contract1 : SmartContract
    {
        private static readonly byte[] Me = "NfKA6zAixybBHHpmaPYPDywoqDaKzfMPf9".ToScriptHash();

        public static object Test(string operation)
        {
            var neoDecimals = (BigInteger)Native.NEO("decimals", new object[] { Me });
            var gasDecimals = (BigInteger)Native.GAS("decimals", new object[] { Me });
            int neoRatio = 1;
            for (int i = 0; i < neoDecimals; i++) neoRatio *= 10;
            int gasRatio = 1;
            for (int i = 0; i < gasDecimals; i++) gasRatio *= 10;

            if (operation == "getNeoBalance")
            {
                return (BigInteger)Native.NEO("balanceOf", new object[] { Me }) / neoRatio;
            }
            if (operation == "getGasBalance")
            {
                return (BigInteger)Native.GAS("balanceOf", new object[] { Me }) / gasRatio;
            }
            if (operation == "getNeoTotalSupply")
            {
                return (BigInteger)Native.NEO("totalSupply", new object[] { Me }) / neoRatio;
            }
            if (operation == "getGasTotalSupply")
            {
                return (BigInteger)Native.GAS("totalSupply", new object[] { Me }) / gasRatio;
            }
            if (operation == "getNeoDecimals")
            {
                return neoDecimals;
            }
            if (operation == "getGasDecimals")
            {
                return gasDecimals;
            }
            return true;
        }
    }
}
