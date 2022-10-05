using DataToCache.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace DataToCache
{
    internal class CacheProvider
    {
        private const string FILENAME = "data.cache";

        private readonly static byte[] additionalEntropy = new byte[5] { 6, 4, 23, 89, 1 };

        public void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new(typeof(List<ConnectionString>));
                using MemoryStream memoryStream = new();
                using XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
                xmlSerializer.Serialize(xmlWriter, connections);
                byte[] protectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes(FILENAME, protectedData);
            }
            catch
            {
                Console.WriteLine("Serialize data error");
                throw new SerializeDataCacheProviderException();
            }
        }

        public List<ConnectionString> GetConnectionsFromCache()
        {
            try
            {
                XmlSerializer xmlSerializer = new(typeof(List<ConnectionString>));
                byte[] protectedData = File.ReadAllBytes(FILENAME);
                byte[] data = Unprotect(protectedData);

                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data))!;
            }
            catch
            {
                Console.WriteLine("Deserialize data error");
                throw new DeserializeDataCacheProviderException();
            }
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch
            {
                Console.WriteLine("Unprotect error");
                throw new UnprotectCacheProviderException();
            }
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch
            {
                Console.WriteLine("Protected error");
                throw new ProtectCacheProviderException();
            }
        }

    }
}
