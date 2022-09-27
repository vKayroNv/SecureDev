namespace DataToCache
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ConnectionString> connections = new()
            {
                new()
                {
                    DatabaseName = "db1",
                    Host = "localhost",
                    UserName = "user",
                    Password = "123456",
                },
                new()
                {
                    DatabaseName = "db2",
                    Host = "127.0.0.1",
                    UserName = "admin",
                    Password = "password",
                }
            };

            CacheProvider cacheProvider = new();
            cacheProvider.CacheConnections(connections);

            connections = cacheProvider.GetConnectionsFromCache();

            foreach (var connection in connections)
                Console.WriteLine(connection);

            Console.ReadKey(true);
        }
    }
}