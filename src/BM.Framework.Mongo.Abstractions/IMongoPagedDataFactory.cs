using BM.Framework.Mongo.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BM.Framework.Mongo
{
    public interface IMongoPagedDataFactory
    {
        ValueTask<IPagedData<T>> CreateAsync<T>(
            IQueryable<T> query, int page, int perPage,
            CancellationToken cancellationToken = default);
    }
}
