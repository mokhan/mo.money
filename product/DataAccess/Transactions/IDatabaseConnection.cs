using System;
using System.Collections.Generic;

namespace MoMoney.DataAccess.Transactions
{
    public interface IDatabaseConnection : IDisposable
    {
        IEnumerable<T> query<T>();
        IEnumerable<T> query<T>(Predicate<T> predicate);
        void delete<T>(T entity);
        void commit();
        void store<T>(T entity);
    }
}