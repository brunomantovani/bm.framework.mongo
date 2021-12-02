using BM.Framework.Mongo.Tests.Fakes.Data.Configurations;
using BM.Framework.Mongo.Tests.Fakes.DomainModels;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BM.Framework.Mongo.Tests.Fakes.Data
{
    public class SampleContext
        : MongoContext
    {
        public SampleContext(
            MongoSettings settings)
            : base(settings)
        {
        }

        public SampleContext(
            IMongoClient client,
            string databaseName)
            : base(client, databaseName)
        {
        }

        public override void OnConfiguring(IMongoDocumentBuilder builder)
        {
            builder.ApplyConfiguration(new PhysicalPersonConfigution());

            //Exemplo de criação de índice no OnConfiguring
            //**** É necessário colocar após o(s) ApplyConfiguration(s), 
            //  pois se não irá ignorar o que foi mapeado.
            GetCollection<PhysicalPerson>()
                .Indexes.CreateMany(new[]
                {
                    new CreateIndexModel<PhysicalPerson>(
                        keys: Builders<PhysicalPerson>.IndexKeys.Descending(x => x.CreatedAt),
                        options: new CreateIndexOptions<PhysicalPerson>
                        {
                            Version = 1,
                            Name = "CreatedAtAscIndex"
                        }),

                    new CreateIndexModel<PhysicalPerson>(
                        keys: Builders<PhysicalPerson>.IndexKeys.Ascending(x => x.Active),
                        options: new CreateIndexOptions<PhysicalPerson>
                        {
                            Version = 1,
                            Name = "ActiveAscIndex"
                        })
                });
        }

        public IMongoRepository CreateRepository()
        {
            var logger = new LoggerFactory().CreateLogger<MongoRepository>();
            return new MongoRepository(logger, this);
        }
    }
}
