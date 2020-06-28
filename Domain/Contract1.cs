using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System.ComponentModel;

namespace Domain
{
    [ManifestExtra("Author", "Chen, Zhitong")]
    [ManifestExtra("Email", "chenzhitong@onchain.com")]
    [ManifestExtra("Description", "This is a Domain example")]
    [Features(ContractFeatures.HasStorage)]
    public class Contract1 : SmartContract
    {
        public static byte[] Query(string domain) => DomainStorage.Get(domain);

        public static bool Register(string domain, byte[] owner)
        {
            if (DomainStorage.Exist(domain)) return false;
            if (!Runtime.CheckWitness(owner)) return false;
            DomainStorage.Put(domain, owner);
            return true;
        }

        public static bool Transfer(string domain, byte[] to)
        {
            if (DomainStorage.Exist(domain)) return false;
            if (!Runtime.CheckWitness(to)) return false;
            if (!IsDomainOwner(domain)) return false;
            DomainStorage.Put(domain, to);
            return true;
        }

        public static bool Remove(string domain)
        {
            if (DomainStorage.Exist(domain)) return false;
            if (!IsDomainOwner(domain)) return false;
            DomainStorage.Remove(domain);
            return true;
        }

        private static bool IsDomainOwner(string key) => Runtime.CheckWitness(DomainStorage.Get(key));
    }
}