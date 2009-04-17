using System;
using System.Linq;
using System.Collections.Generic;
using Gorilla.Commons.Utility.Extensions;
using MoMoney.Domain.Core;

namespace MoMoney.Infrastructure.transactions2
{
    public interface ITransaction
    {
        IIdentityMap<Guid, T> create_for<T>() where T : IEntity;
        void commit_changes();
        void rollback_changes();
        bool is_dirty();
    }

    public class Transaction : ITransaction
    {
        readonly IDatabase database;
        readonly IChangeTrackerFactory factory;
        readonly IDictionary<Type, IChangeTracker> change_trackers;

        public Transaction(IDatabase database, IChangeTrackerFactory factory)
        {
            this.factory = factory;
            this.database = database;
            change_trackers = new Dictionary<Type, IChangeTracker>();
        }

        public IIdentityMap<Guid, T> create_for<T>() where T : IEntity
        {
            return new IdentityMapProxy<Guid, T>(get_change_tracker_for<T>(), new IdentityMap<Guid, T>());
        }

        public void commit_changes()
        {
            change_trackers.Values.where(x => x.is_dirty()).each(x => x.commit_to(database));
        }

        public void rollback_changes()
        {
            change_trackers.each(x => x.Value.Dispose());
            change_trackers.Clear();
        }

        public bool is_dirty()
        {
            return change_trackers.Values.Count(x => x.is_dirty()) > 0;
        }

        IChangeTracker<T> get_change_tracker_for<T>() where T : IEntity
        {
            if (!change_trackers.ContainsKey(typeof (T))) change_trackers.Add(typeof (T), factory.create_for<T>());
            return change_trackers[typeof (T)].downcast_to<IChangeTracker<T>>();
        }
    }
}