using System.Collections.Generic;
using momoney.database.transactions;
using MoMoney.Domain.accounting;
using MoMoney.Domain.repositories;

namespace momoney.database.repositories
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
        readonly ISession session;

        public AccountHolderRepository(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<IAccountHolder> all()
        {
            return session.all<IAccountHolder>();
        }

        public void save(IAccountHolder account_holder)
        {
            session.save(account_holder);
        }
    }
}