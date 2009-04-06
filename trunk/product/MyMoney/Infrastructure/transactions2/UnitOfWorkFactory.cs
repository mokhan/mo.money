using MoMoney.Utility.Core;

namespace MoMoney.Infrastructure.transactions2
{
    public interface IUnitOfWorkFactory : IFactory<IUnitOfWork>
    {
    }

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        readonly IContext context;
        readonly ISessionFactory factory;
        readonly IKey<ISession> key;

        public UnitOfWorkFactory(IContext current_request, ISessionFactory factory, IKey<ISession> key)
        {
            context = current_request;
            this.key = key;
            this.factory = factory;
        }

        public IUnitOfWork create()
        {
            return unit_of_work_been_started() ? new EmptyUnitOfWork() : start_a_new_unit_of_work();
        }

        bool unit_of_work_been_started()
        {
            return context.contains(key);
        }

        IUnitOfWork start_a_new_unit_of_work()
        {
            var session = factory.create();
            context.add(key, session);
            return new UnitOfWork(session, context, key);
        }
    }
}