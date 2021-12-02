using MongoDB.Bson.Serialization;

namespace BM.Framework.Mongo
{
    public interface IMongoDocumentConfiguration<TMongoDocument>
        where TMongoDocument : class
    {
        void Configure(BsonClassMap<TMongoDocument> builder);
    }
}