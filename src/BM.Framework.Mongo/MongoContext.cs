using BM.Framework.Mongo.Internal;
using MongoDB.Driver;

namespace BM.Framework.Mongo
{
    public abstract class MongoContext
    {
        private static bool _configured = false;

        internal readonly IMongoClient _client;        
        internal readonly IMongoDatabase _database;

        private void Configure()
        {
            if(!_configured)
            {
                OnConfiguring(new MongoDocumentBuilder());
                _configured = true;
            }            
        }

        protected MongoContext(
            IMongoClient client,
            string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new System.ArgumentException(Resources.IsNullOrWhiteSpace, nameof(databaseName));
            }

            _client = client ?? throw new System.ArgumentNullException(nameof(client));
            _database = client.GetDatabase(databaseName);

            Configure();
        }

        protected MongoContext(
            MongoSettings settings)
        {
            if (settings is null)
            {
                throw new System.ArgumentNullException(nameof(settings));
            }

            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);

            Configure();
        }

        public IMongoCollection<TMongoDocument> GetCollection<TMongoDocument>()
        {
            var name = typeof(TMongoDocument).Name;
            return _database.GetCollection<TMongoDocument>(name);
        }

        public virtual IMongoCollection<TMongoDocument> GetCollection<TMongoDocument>(string name)
        {
            return _database.GetCollection<TMongoDocument>(name);
        }

        public virtual void OnConfiguring(
            IMongoDocumentBuilder builder)
        {
        }
    }
}
