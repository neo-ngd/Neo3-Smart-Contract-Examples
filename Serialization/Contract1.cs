using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Serialization
{
    [ManifestExtra("Author", "Chen, Zhitong")]
    [ManifestExtra("Email", "chenzhitong@onchain.com")]
    [ManifestExtra("Description", "This is a Serialization example")]
    public class Contract1 : SmartContract
    {
        public static uint Test()
        {
            var news = new News() { Title = "Hello", Content = "World", BlockHeight = Blockchain.GetHeight() };
            var s = news.Serialize();
            var d = s.Deserialize() as News;

            //return $"{d.BlockHeight} {d.Title} {d.Content}"; 这么写会报错
            //return d.BlockHeight + d.Title + d.Content; 这么写会报错
            //return d.BlockHeight.ToString(); 这么写会报错，抓狂

            return d.BlockHeight;
        }
    }
}
