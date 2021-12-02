using MongoDB.Bson.Serialization;

namespace BM.Framework.Mongo.Internal
{
    internal class MongoDocumentBuilder
        : IMongoDocumentBuilder
    {
        void IMongoDocumentBuilder.ApplyConfiguration<TMongoDocument>(
            IMongoDocumentConfiguration<TMongoDocument> configuration)
        {
            if (configuration is null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }

            if (BsonClassMap.IsClassMapRegistered(typeof(TMongoDocument)))
            {
                return;
            }

            BsonClassMap.RegisterClassMap<TMongoDocument>(configuration.Configure);
        }
    }
}
