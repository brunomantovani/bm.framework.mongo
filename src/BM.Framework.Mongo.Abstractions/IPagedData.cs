using System.Collections.Generic;

namespace BM.Framework.Mongo.Abstractions
{
    public interface IPagedData<T>
    {
        IEnumerable<T> Items { get; }
        int CurrentPage { get; }
        int NextPage { get; }
        int PreviousPage { get; }
        int TotalPages { get; }
        int TotalCount { get; }
    }
}
