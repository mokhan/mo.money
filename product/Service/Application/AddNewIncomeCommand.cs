using System.Linq;
using Gorilla.Commons.Utility.Core;
using Gorilla.Commons.Utility.Extensions;
using MoMoney.Domain.Core;
using MoMoney.Domain.repositories;
using MoMoney.DTO;
using MoMoney.Service.Contracts.Application;

namespace MoMoney.Service.Application
{
    public class AddNewIncomeCommand : IAddNewIncomeCommand
    {
        readonly IGetTheCurrentCustomerQuery tasks;
        readonly INotification notification;
        readonly IIncomeRepository all_income;
        readonly ICompanyRepository companys;

        public AddNewIncomeCommand(IGetTheCurrentCustomerQuery tasks, INotification notification, IIncomeRepository all_income,
                                   ICompanyRepository companys)
        {
            this.tasks = tasks;
            this.notification = notification;
            this.all_income = all_income;
            this.companys = companys;
        }

        public void run(IncomeSubmissionDTO item)
        {
            if (similar_income_has_been_submitted(item))
            {
                notification.notify("You have already submitted this income");
            }
            else
            {
                companys
                    .find_company_by(item.company_id)
                    .pay(
                    tasks.fetch(),
                    item.amount.as_money(),
                    item.recieved_date
                    );
            }
        }

        bool similar_income_has_been_submitted(IncomeSubmissionDTO income)
        {
            if (all_income.all().Count() == 0) return false;
            return all_income
                       .all()
                       .where(x => x.amount_tendered.Equals(income.amount.as_money()))
                       .where(x => x.company.id.Equals(income.company_id))
                       .where(x => x.date_of_issue.Equals(income.recieved_date))
                       .Count() > 0;
        }
    }
}