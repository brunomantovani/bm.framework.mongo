namespace BM.Framework.Mongo
{
    public class MongoSettings
    {
        private MongoSettings()
        {
        }

        public MongoSettings(
            string connectionString,
            string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }
    }
}
