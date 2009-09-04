using System.Collections.Generic;
using System.Linq;
using MoMoney.DTO;
using MoMoney.Presentation.Presenters.billing;
using MoMoney.Presentation.Views.core;

namespace MoMoney.Presentation.Views.billing
{
    public partial class ViewAllBills : ApplicationDockedWindow, IViewAllBills
    {
        public ViewAllBills()
        {
            InitializeComponent();
            titled("View Bills");
        }

        public void attach_to(IViewAllBillsPresenter presenter)
        {
        }

        public void run(IEnumerable<BillInformationDTO> bills)
        {
            ux_bills.DataSource = bills.ToList();
        }
    }
}