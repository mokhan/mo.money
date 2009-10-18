using System;
using System.Collections.Generic;
using Gorilla.Commons.Utility.Core;

namespace MoMoney.DataAccess.Transactions
{
    public interface IDatabase
    {
        IEnumerable<T> fetch_all<T>() where T : IIdentifiable<Guid>;
        void apply(IStatement statement);
    }
}