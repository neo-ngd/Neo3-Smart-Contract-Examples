using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Dynamic_Call
{
    [ManifestExtra("Author", "Chen, Zhitong")]
    [ManifestExtra("Email", "chenzhitong@onchain.com")]
    [ManifestExtra("Description", "This is a NEP5 example")]
    public class Contract1 : SmartContract
    {
        //HexToBytes()、ToScriptHash() 只能对常量进行操作，不能写在方法里
        //scriptHash 也可以改为从参数传入或从存储区中读取
        static readonly byte[] ScriptHash = "0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9".HexToBytes();

        public static object Call(string operation, object[] args) => Contract.Call(ScriptHash, operation, args);
    }
}
