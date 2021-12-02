using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BM.Framework.Mongo
{
    public interface IMongoRepository
    {
        IQueryable<T> AsQueryable<T>() where T : class;

        Task AddAsync<TAggregate>(
            TAggregate document,
            CancellationToken cancellationToken = default)
            where TAggregate : class;

        Task AddRangeAsync<TAggregate>(
            IEnumerable<TAggregate> documents,
            CancellationToken cancellationToken = default)
            where TAggregate : class;

        Task RemoveAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            CancellationToken cancellationToken = default)
            where TAggregate : class;

        Task RemoveRangeAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            CancellationToken cancellationToken = default)
            where TAggregate : class;

        Task UpdateAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            TAggregate document,
            CancellationToken cancellationToken = default)
            where TAggregate : class;

        Task UpdateRangeAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            IEnumerable<TAggregate> documents,
            CancellationToken cancellationToken = default)
            where TAggregate : class;
    }
}
