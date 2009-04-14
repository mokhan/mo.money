using System.Collections.Generic;
using MoMoney.Domain.accounting.billing;
using MoMoney.Presentation.Core;
using MoMoney.Presentation.Presenters.billing.dto;
using MoMoney.Presentation.Views.billing;
using MoMoney.Tasks.application;
using MoMoney.Tasks.infrastructure.core;

namespace MoMoney.Presentation.Presenters.billing
{
    public interface IAddBillPaymentPresenter : IContentPresenter
    {
        void submit_bill_payment(AddNewBillDTO dto);
    }

    public class AddBillPaymentPresenter : ContentPresenter<IAddBillPaymentView>, IAddBillPaymentPresenter
    {
        readonly IBillingTasks tasks;
        readonly ICommandPump pump;

        public AddBillPaymentPresenter(IAddBillPaymentView view, ICommandPump pump, IBillingTasks tasks) : base(view)
        {
            this.pump = pump;
            this.tasks = tasks;
        }

        public override void run()
        {
            view.attach_to(this);
            pump
                .run<IEnumerable<ICompany>, IGetAllCompanysQuery>(view)
                .run<IEnumerable<BillInformationDTO>, IGetAllBillsQuery>(view);
        }

        public void submit_bill_payment(AddNewBillDTO dto)
        {
            pump
                .run<ISaveNewBillCommand, AddNewBillDTO>(dto)
                .run<IEnumerable<BillInformationDTO>, IGetAllBillsQuery>(view);
        }
    }
}