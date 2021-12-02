namespace BM.Framework.Mongo
{
    public interface IMongoDocumentBuilder
    {
        void ApplyConfiguration<TMongoDocument>(
            IMongoDocumentConfiguration<TMongoDocument> configuration)
            where TMongoDocument : class;
    }
}
