using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BM.Framework.Mongo.Tests.Fakes.Data;
using BM.Framework.Mongo.Tests.Fakes.DomainModels;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BM.Framework.Mongo.Tests
{
    [TestClass]
    public class MongoRepositoryTest
    {
        private readonly IMongoRepository _repository;
        private readonly CancellationToken _cancellationToken;

        public MongoRepositoryTest()
        {
            var context = SampleSettings.CreateContext();
            _repository = context.CreateRepository();
            _cancellationToken = new CancellationToken();
        }

        [TestInitialize]
        public async Task Initialize()
        {
            await SampleSettings.ClearDatabaseAsync();
        }

        [TestMethod]
        public void AssertCreateAsync()
        {
            //arrange
            var logger = new LoggerFactory().CreateLogger<MongoRepository>();
            var settings = new SampleSettings();
            var context = new SampleContext(settings);
            MongoRepository mongoRepository;

            //act
            mongoRepository = new MongoRepository(logger, context);

            //assert
            Assert.IsNotNull(mongoRepository);
        }

        [TestMethod]
        public void AssertQueryable()
        {
            //arrange

            //act
            _repository
                .AsQueryable<PhysicalPerson>()
                .Count();

            //assert            
        }

        [TestMethod]
        public async Task AssertAddAsync()
        {
            //arrange
            var cpf = new Cpf("191.000.000-10");
            var fullName = new FullName("New Physical Person");
            var document = new PhysicalPerson(cpf, fullName);

            //act
            await _repository.AddAsync(
                document: document);

            //assert
            var result = _repository
                .AsQueryable<PhysicalPerson>()
                .SingleOrDefault(x => x.Cpf == cpf);

            Assert.IsNotNull(result);
            Assert.AreEqual(document.Cpf, result.Cpf);
            Assert.AreEqual(document.Active, result.Active);
            Assert.AreEqual(document.FullName, result.FullName);
        }

        [TestMethod]
        public async Task AssertAddRangeAsync()
        {
            //arrange
            var cpf1 = new Cpf("291.000.000-10");
            var cpf2 = new Cpf("291.000.000-20");

            var fullName = new FullName("To Add");
            var documents = new PhysicalPerson[]
            {
                new PhysicalPerson(cpf1, fullName),
                new PhysicalPerson(cpf2, fullName),
            };

            await _repository.RemoveRangeAsync<PhysicalPerson>(
                filter: x => x.FullName == fullName,
                cancellationToken: _cancellationToken);

            //act
            await _repository
                .AddRangeAsync(
                    documents: documents,
                    cancellationToken: _cancellationToken);

            //assert
            var result = _repository
                .AsQueryable<PhysicalPerson>()
                .Where(x => x.FullName == fullName)
                .ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(documents.Length, result.Length);
        }

        [TestMethod]
        public async Task AssertUpdateAsync()
        {
            //arrange
            var cpf = new Cpf("191.000.000-00");
            var fullName = new FullName("Full Name");
            var newFullName = new FullName("New FullName");
            var document = new PhysicalPerson(cpf, fullName);

            await _repository.AddAsync(document, _cancellationToken);

            document.ChangeFullName(newFullName);

            //act
            await _repository.UpdateAsync(
                filter: x => x.Cpf == cpf,
                document: document,
                cancellationToken: _cancellationToken);

            //assert
            var result = _repository
                .AsQueryable<PhysicalPerson>()
                .Where(x => x.FullName == newFullName)
                .Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task AssertUpdateRangeAsync()
        {
            //arrange            
            var cpf70 = new Cpf("591.000.000-10");
            var cpf80 = new Cpf("591.000.000-20");

            var fullName = new FullName("Update Range");
            var newFullName = new FullName("New Update Range");

            var query = _repository
                .AsQueryable<PhysicalPerson>();

            var documentsToUpdate = new PhysicalPerson[]
            {
                new PhysicalPerson(cpf70, fullName),
                new PhysicalPerson(cpf80, fullName)
            };

            await _repository.AddRangeAsync(
                documents: documentsToUpdate,
                cancellationToken: _cancellationToken);

            var addedCount = query.Where(x => x.FullName == fullName).Count();

            documentsToUpdate[0].ChangeFullName(newFullName);
            documentsToUpdate[1].ChangeFullName(newFullName);

            //act
            await _repository.UpdateRangeAsync(
                filter: x => x.FullName == fullName,
                documents: documentsToUpdate,
                cancellationToken: _cancellationToken);

            //assert
            var resultCount = query.Where(x => x.FullName == newFullName).Count();

            Assert.AreEqual(addedCount, resultCount);
        }

        [TestMethod]
        public async Task AssertRemoveAsync()
        {
            //arrange
            var cpf = new Cpf("191.000.000-99");
            var fullName = new FullName("Remove This");
            var document = new PhysicalPerson(cpf, fullName);

            await _repository.AddAsync(
                document: document,
                cancellationToken: _cancellationToken);

            //act
            await _repository
                .RemoveAsync<PhysicalPerson>(
                    filter: x => x.Cpf == cpf,
                    cancellationToken: _cancellationToken);

            //assert
            var result = _repository
                .AsQueryable<PhysicalPerson>()
                .SingleOrDefault(x => x.Cpf == cpf);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AssertRemoveRangeAsync()
        {
            //arrange
            //arrange
            var cpf1 = new Cpf("791.000.000-10");
            var cpf2 = new Cpf("791.000.000-20");

            var fullName = new FullName("Remove Range");
            var documents = new PhysicalPerson[]
            {
                new PhysicalPerson(cpf1, fullName),
                new PhysicalPerson(cpf2, fullName),
            };

            await _repository.AddRangeAsync(
                documents: documents,
                cancellationToken: _cancellationToken);

            //act
            await _repository
                .RemoveRangeAsync<PhysicalPerson>(
                    filter: x => x.FullName == fullName,
                    cancellationToken: _cancellationToken);

            //assert
            var countResult = _repository
                .AsQueryable<PhysicalPerson>()
                .Count(x => x.FullName == fullName);

            Assert.AreEqual(0, countResult);
        }
    }
}
