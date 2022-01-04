using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace EnLock
{
    public static class EnExtention
    {
        public static async Task<bool> ToAnyWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            bool result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.AnyAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
        public static async Task<T[]> ToArrayWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            T[] result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.ToArrayAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
        public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            List<T> result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.ToListAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
        public static async Task<T> ToFirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            T result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.FirstOrDefaultAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
        public static async Task<T> ToFirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            T result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.FirstOrDefaultAsync(predicate, cancellationToken);
                scope.Complete();
            }
            return result;
        }
        public static async Task<T> ToFirstWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            T result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.FirstAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
        public static async Task<T> ToSingleWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            T result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.SingleAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
    }
}
