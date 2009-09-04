using MoMoney.Presentation.Core;
using MoMoney.Presentation.Views.billing;
using MoMoney.Presentation.Views.reporting;
using MoMoney.Service.Application;

namespace MoMoney.Presentation.Presenters.reporting
{
    public interface IViewAllBillsReportPresenter : IContentPresenter
    {
    }

    public class ReportPresenter : ContentPresenter<IReportViewer>, IViewAllBillsReportPresenter
    {
        readonly IViewAllBillsReport report;
        readonly IGetAllBillsQuery query;

        public ReportPresenter(IReportViewer view, IViewAllBillsReport report, IGetAllBillsQuery query) : base(view)
        {
            this.report = report;
            this.query = query;
        }

        public override void run()
        {
            report.run(query.fetch());
            view.display(report);
        }
    }
}