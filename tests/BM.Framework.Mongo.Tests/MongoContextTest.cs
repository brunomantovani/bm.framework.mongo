using BM.Framework.Mongo.Tests.Fakes.Data;
using BM.Framework.Mongo.Tests.Fakes.DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace BM.Framework.Mongo.Tests
{
    [TestClass]
    public class MongoContextTest
    {
        private MongoSettings _settings;
        private MongoContext _mongoContext;

        [TestInitialize]
        public void Initialize()
        {
            _settings = new SampleSettings();
            _mongoContext = new SampleContext(_settings);
        }

        [TestMethod]
        public void AssertCreate()
        {
            //arrange
            MongoContext mongoContext;

            //act
            mongoContext = new SampleContext(_settings);

            //assert
            Assert.IsNotNull(mongoContext);
            Assert.IsInstanceOfType(mongoContext, typeof(MongoContext));
        }

        [TestMethod]
        public void AssertGetCollectionByTypeName()
        {
            //arrange
            IMongoCollection<PhysicalPerson> collection;

            //act
            collection = _mongoContext.GetCollection<PhysicalPerson>();

            //assert
            Assert.IsNotNull(collection);
        }

        [TestMethod]
        public void AssertGetCollectionByName()
        {
            //arrange
            var name = nameof(PhysicalPerson);
            IMongoCollection<PhysicalPerson> collection;

            //act
            collection = _mongoContext.GetCollection<PhysicalPerson>(name);

            //assert
            Assert.IsNotNull(collection);
        }
    }
}
