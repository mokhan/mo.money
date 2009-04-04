using System.Collections.Generic;
using System.Linq;
using MoMoney.Utility.Extensions;

namespace MoMoney.Infrastructure.transactions2
{
    public class ChangeTracker<T> : IChangeTracker<T>
    {
        readonly ITrackerEntryMapper<T> mapper;
        readonly IStatementRegistry registry;
        readonly IList<ITrackerEntry<T>> items;
        readonly IList<T> to_be_deleted;

        public ChangeTracker(ITrackerEntryMapper<T> mapper, IStatementRegistry registry)
        {
            this.mapper = mapper;
            this.registry = registry;
            items = new List<ITrackerEntry<T>>();
            to_be_deleted = new List<T>();
        }

        public void register(T entity)
        {
            items.Add(mapper.map_from(entity));
        }

        public void delete(T entity)
        {
            to_be_deleted.Add(entity);
        }

        public void commit_to(IDatabase database)
        {
            items.each(x => commit(x, database));
            to_be_deleted.each(x => database.apply(registry.prepare_command_for(x)));
        }

        public bool is_dirty()
        {
            return items.Count(x => x.contains_changes()) > 0 || to_be_deleted.Count > 0;
        }

        public void Dispose()
        {
            items.Clear();
        }

        void commit(ITrackerEntry<T> entry, IDatabase database)
        {
            if (entry.contains_changes()) database.apply(registry.prepare_command_for(entry.current));
        }
    }
}