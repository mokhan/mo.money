using gorilla.commons.utility;
using MoMoney.Domain.repositories;

namespace MoMoney.Domain.Accounting
{
    public interface ICompanyFactory : Factory<ICompany> {}

    public class CompanyFactory : ICompanyFactory
    {
        readonly ComponentFactory<Company> factory;
        readonly ICompanyRepository companys;

        public CompanyFactory(ComponentFactory<Company> factory, ICompanyRepository companys)
        {
            this.factory = factory;
            this.companys = companys;
        }

        public ICompany create()
        {
            var company = factory.create();
            companys.save(company);
            return company;
        }
    }
}