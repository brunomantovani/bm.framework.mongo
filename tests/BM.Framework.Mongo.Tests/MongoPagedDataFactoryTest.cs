using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BM.Framework.Mongo.Tests
{
    [TestClass]
    public class MongoPagedDataFactoryTest
    {
        private IMongoPagedDataFactory _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new MongoPagedDataFactory();
        }

        [TestMethod]
        public void AssertCreate()
        {
            //arrange
            IMongoPagedDataFactory factory;

            //act
            factory = new MongoPagedDataFactory();

            //assert
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public async Task AssertCreateAsync()
        {
            //arrange
            var items = new string[]
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5",
                "Item 6",
                "Item 7",
                "Item 8",
                "Item 9",
            };

            var page1 = 1;
            var perPage = 4;
            var query = items.AsQueryable();
             

            //act
            var result = await _factory
                .CreateAsync(query, page1, perPage);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(page1 + 1, result.NextPage);
            Assert.AreEqual(page1, result.CurrentPage);
            Assert.AreEqual(perPage, result.Items.Count());
            Assert.AreEqual(page1, result.PreviousPage);

            var resultDivRem = Math.DivRem(items.Length, perPage, out int resultRemainder);
            var totalPages =  resultDivRem + (resultRemainder > 0 ? 1 : 0);
            Assert.AreEqual(totalPages, result.TotalPages);
        }
    }
}
