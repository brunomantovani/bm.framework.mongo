using BM.Framework.Mongo.Tests.Fakes.DomainModels;
using MongoDB.Bson.Serialization;

namespace BM.Framework.Mongo.Tests.Fakes.Data.Configurations
{
    public class PhysicalPersonConfigution
        : IMongoDocumentConfiguration<PhysicalPerson>
    {
        public void Configure(BsonClassMap<PhysicalPerson> builder)
        {
            builder.AutoMap();
            builder.MapIdMember(x => x.Cpf);
        }
    }
}
