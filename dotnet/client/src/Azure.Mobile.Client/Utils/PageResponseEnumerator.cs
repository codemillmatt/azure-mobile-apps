﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace Azure.Mobile.Client.Utils
{
    /// <summary>
    /// This is a direct copy of the PageResponseEnumerator in the Azure.Core Shared source library
    /// </summary>
    internal static class PageResponseEnumerator
    {
        public static FuncPageable<T> CreateEnumerable<T>(Func<string?, Page<T>> pageFunc) where T : notnull
        {
            return new FuncPageable<T>((continuationToken, pageSizeHint) => pageFunc(continuationToken));
        }

        //public static FuncPageable<T> CreateEnumerable<T>(Func<string?, int?, Page<T>> pageFunc) where T : notnull
        //{
        //    return new FuncPageable<T>(pageFunc);
        //}

        public static AsyncPageable<T> CreateAsyncEnumerable<T>(Func<string?, Task<Page<T>>> pageFunc) where T : notnull
        {
            return new FuncAsyncPageable<T>((continuationToken, pageSizeHint) => pageFunc(continuationToken));
        }

        //public static AsyncPageable<T> CreateAsyncEnumerable<T>(Func<string?, int?, Task<Page<T>>> pageFunc) where T : notnull
        //{
        //    return new FuncAsyncPageable<T>(pageFunc);
        //}

        internal class FuncAsyncPageable<T> : AsyncPageable<T> where T : notnull
        {
            private readonly Func<string?, int?, Task<Page<T>>> _pageFunc;

            public FuncAsyncPageable(Func<string?, int?, Task<Page<T>>> pageFunc)
            {
                _pageFunc = pageFunc;
            }

            public override async IAsyncEnumerable<Page<T>> AsPages(string? continuationToken = default, int? pageSizeHint = default)
            {
                do
                {
                    Page<T> pageResponse = await _pageFunc(continuationToken, pageSizeHint).ConfigureAwait(false);
                    yield return pageResponse;
                    continuationToken = pageResponse.ContinuationToken;
                } while (continuationToken != null);
            }
        }

        internal class FuncPageable<T> : Pageable<T> where T : notnull
        {
            private readonly Func<string?, int?, Page<T>> _pageFunc;

            public FuncPageable(Func<string?, int?, Page<T>> pageFunc)
            {
                _pageFunc = pageFunc;
            }

            public override IEnumerable<Page<T>> AsPages(string? continuationToken = default, int? pageSizeHint = default)
            {
                do
                {
                    Page<T> pageResponse = _pageFunc(continuationToken, pageSizeHint);
                    yield return pageResponse;
                    continuationToken = pageResponse.ContinuationToken;
                } while (continuationToken != null);
            }
        }
    }
}
