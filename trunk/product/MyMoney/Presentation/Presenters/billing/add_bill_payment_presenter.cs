using MyMoney.Domain.accounting.billing;
using MyMoney.Presentation.Core;
using MyMoney.Presentation.Presenters.billing.dto;
using MyMoney.Presentation.Views.billing;
using MyMoney.Presentation.Views.core;
using MyMoney.Tasks.application;
using MyMoney.Utility.Extensions;

namespace MyMoney.Presentation.Presenters.billing
{
    public interface IAddBillPaymentPresenter : IContentPresenter
    {
        void submit_bill_payment(add_new_bill_dto dto);
    }

    public class add_bill_payment_presenter : IAddBillPaymentPresenter
    {
        private readonly IAddBillPaymentView view;
        private readonly IBillingTasks tasks;

        public add_bill_payment_presenter(IAddBillPaymentView view, IBillingTasks tasks)
        {
            this.view = view;
            this.tasks = tasks;
        }

        public void run()
        {
            view.attach_to(this);
            view.display(tasks.all_companys());
            view.display(tasks.all_bills().map_all_using(x => map_from(x)));
        }

        public void submit_bill_payment(add_new_bill_dto dto)
        {
            tasks.save_a_new_bill_using(dto);
            view.display(tasks.all_bills().map_all_using(x => map_from(x)));
        }

        private bill_information_dto map_from(IBill bill)
        {
            return new bill_information_dto
                       {
                           company_name = bill.company_to_pay.name,
                           the_amount_owed = bill.the_amount_owed.ToString(),
                           due_date = bill.due_date.to_date_time(),
                       };
        }

        IDockedContentView IContentPresenter.View
        {
            get { return view; }
        }
    }
}