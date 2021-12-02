using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BM.Framework.Mongo
{
    public class MongoRepository
        : IMongoRepository
    {
        private readonly ILogger<MongoRepository> _logger;
        private readonly MongoContext _mongoContext;

        public MongoRepository(
            ILogger<MongoRepository> logger,
            MongoContext mongoContext)
        {
            _logger = logger;
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
        }

        IQueryable<TMongoDocument> IMongoRepository.AsQueryable<TMongoDocument>()
        {
            return _mongoContext.GetCollection<TMongoDocument>().AsQueryable();
        }

        async Task IMongoRepository.AddAsync<TAggregate>(
            TAggregate aggregate,
            CancellationToken cancellationToken)
        {
            await _mongoContext.GetCollection<TAggregate>()
                .InsertOneAsync(aggregate, null, cancellationToken);
        }

        Task IMongoRepository.AddRangeAsync<TAggregate>(
            IEnumerable<TAggregate> aggregates,
            CancellationToken cancellationToken)
        {
            return _mongoContext.GetCollection<TAggregate>()
                .InsertManyAsync(aggregates, null, cancellationToken);
        }

        Task IMongoRepository.UpdateAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            TAggregate aggregate,
            CancellationToken cancellationToken)
        {
            ReplaceOptions options = null;

            return _mongoContext.GetCollection<TAggregate>()
                .ReplaceOneAsync(
                    filter: filter,
                    replacement: aggregate,
                    options: options,
                    cancellationToken: cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        _logger.LogDebug("Documentos atualizados: {0}", t.Result.ModifiedCount);
                    }

                    if (t.IsFaulted)
                    {
                        _logger.LogError(t.Exception, "Erro ao atualizar os documentos");
                    }
                });
        }


        Task IMongoRepository.UpdateRangeAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            IEnumerable<TAggregate> aggregates,
            CancellationToken cancellationToken)
        {
            ReplaceOptions options = null;
            var tasks = new List<Task>();

            var collection = _mongoContext.GetCollection<TAggregate>();

            foreach (var aggregate in aggregates)
            {
                var task = collection
                    .ReplaceOneAsync(
                        filter: filter,
                        replacement: aggregate,
                        options: options,
                        cancellationToken: cancellationToken)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            _logger.LogDebug("Documentos modificados: {0}", t.Result.ModifiedCount);
                        }

                        if (t.IsFaulted)
                        {
                            _logger.LogError(t.Exception, "Erro ao atualizar os documentos");
                        }
                    });

                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }

        Task IMongoRepository.RemoveAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            CancellationToken cancellationToken)
        {
            return _mongoContext.GetCollection<TAggregate>()
                .DeleteOneAsync(
                    filter: filter,
                    cancellationToken: cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        _logger.LogDebug("Documentos reovidos: {0}", t.Result.DeletedCount);
                    }

                    if (t.IsFaulted)
                    {
                        _logger.LogError(t.Exception, "Erro ao remover os documentos");
                    }
                });
        }

        Task IMongoRepository.RemoveRangeAsync<TAggregate>(
            Expression<Func<TAggregate, bool>> filter,
            CancellationToken cancellationToken)
        {
            return _mongoContext.GetCollection<TAggregate>()
                .DeleteManyAsync(
                    filter: filter,
                    cancellationToken: cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        _logger.LogDebug("Documentos removidos: {0}", t.Result.DeletedCount);
                    }

                    if (t.IsFaulted)
                    {
                        _logger.LogError(t.Exception, "Erro ao remover os documentos");
                    }
                });


        }
    }
}
