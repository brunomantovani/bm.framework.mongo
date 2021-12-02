using BM.Framework.Mongo.Abstractions;
using System;
using System.Collections.Generic;

namespace BM.Framework.Mongo
{
    public class PagedData<T>
        : IPagedData<T>
    {
        private PagedData()
        {
        }

        public PagedData(
            IEnumerable<T> items,
            int currentPage,
            int totalPages,
            int totalCount)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));

            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Items { get; private set; }

        public int CurrentPage { get; private set; }

        public int NextPage
        {
            get
            {
                if (TotalCount == 0)
                {
                    return TotalPages;
                }

                if (TotalCount > 0 && TotalPages > 1 && CurrentPage < TotalPages)
                {
                    return CurrentPage + 1;
                }

                return CurrentPage;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (TotalCount == 0)
                {
                    if (TotalPages > 1)
                    {
                        return TotalPages - 1;
                    }

                    return 1;
                }
                else if (TotalCount > 0 && CurrentPage > 1)
                {
                    return CurrentPage - 1;
                }

                return CurrentPage;
            }
        }

        public int TotalPages { get; private set; }

        public int TotalCount { get; private set; }
    }
}
