using System.Linq;
using System.Threading.Tasks;
using BM.Framework.Mongo.Tests.Fakes.DomainModels;

namespace BM.Framework.Mongo.Tests.Fakes.Data
{
    public class SampleSettings
        : MongoSettings
    {
        public SampleSettings() : base(
            connectionString: "mongodb://sa:Password@localhost:27017",
            databaseName: "bm_framework_mongo_repository_tests")
        {
        }

        public static SampleSettings Create()
        {
            return new SampleSettings();
        }

        public static SampleContext CreateContext()
        {
            var settings = Create();
            return new SampleContext(settings);
        }

        public static async Task ClearDatabaseAsync()
        {
            var context = CreateContext();
            var repository = context.CreateRepository();
            var toRemoveList = repository
                .AsQueryable<PhysicalPerson>()
                .ToList();

            foreach (var toRemove in toRemoveList)
            {
                await repository.RemoveAsync<PhysicalPerson>(x => x.Cpf == toRemove.Cpf);
            }
        }
    }
}
