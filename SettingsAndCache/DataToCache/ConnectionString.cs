namespace DataToCache
{
    public class ConnectionString
    {
        public string Host { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public override string ToString()
        {
            return $"Host={Host};Catalog={DatabaseName};User Id={UserName};Password={Password}";
        }

    }
}