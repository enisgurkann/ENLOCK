using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace EnLock;

public static class EnExtention
{
    public static async Task<bool> ToAnyWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
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

    public static IQueryable<T> WithNoLock<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
    {
        using var scope = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions()
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            },
            TransactionScopeAsyncFlowOption.Enabled);
        return query;
    }

    public static async Task<T[]> ToArrayWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
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

    public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
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

    public static List<T> ToListWithNoLock<T>(this IQueryable<T> query)
    {
        List<T> result = default;
        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions()
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                   },
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            result = query.ToList();
            scope.Complete();
        }

        return result;
    }

    public static async Task<T> ToFirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
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

    public static async Task<T> ToFirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query,
        Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
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

    public static async Task<T> ToFirstWithNoLockAsync<T>(this IQueryable<T> query,
        Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        T result = default;
        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions()
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                   },
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            result = await query.FirstAsync(predicate, cancellationToken);
            scope.Complete();
        }

        return result;
    }

    public static async Task<T> ToFirstWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
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

    public static async Task<T> ToSingleWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
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

    public static async Task<T> ToSingleWithNoLockAsync<T>(this IQueryable<T> query,
        Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        T result = default;
        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions()
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                   },
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            result = await query.SingleAsync(predicate, cancellationToken);
            scope.Complete();
        }

        return result;
    }


    #region NoLockFunc

    /// <summary>
    /// No Lock tag added
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="dbContext"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static T NoLock<T, TDbContext>(this TDbContext dbContext, Func<TDbContext, T> func)
        where TDbContext : DbContext
    {
        T result = default;
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadUncommitted
        };
        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
        {
            result = func(dbContext);
            scope.Complete();
        }

        return result;
    }

    #endregion
}