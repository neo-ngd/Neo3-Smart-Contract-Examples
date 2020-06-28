using Neo.SmartContract.Framework.Services.Neo;

namespace Domain
{
    public static class DomainStorage
    {
        public static readonly string mapName = "domain";

        public static bool Exist(string key) => Get(key) != null;

        public static byte[] Get(string key) => Storage.CurrentContext.CreateMap(mapName).Get(key);

        public static void Remove(string key) => Storage.CurrentContext.CreateMap(mapName).Delete(key);

        public static void Put(string key, byte[] value) => Storage.CurrentContext.CreateMap(mapName).Put(key, value);
    }
}
