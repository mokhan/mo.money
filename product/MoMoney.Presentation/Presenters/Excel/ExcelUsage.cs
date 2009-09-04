using System.Collections.Generic;
using Gorilla.Commons.Utility.Extensions;
using MoMoney.Presentation.Presenters.Excel;

namespace MoMoney.Presentation.Presenters.excel
{
    public class ExcelUsage
    {
        public IEnumerable<ICellVisitor> run()
        {
            yield return new ConstrainedCellVisitor(
                new ChangeFontSize(8),
                Cell.occurs_between_columns(3, 8)
                    .and(Cell.occurs_after_row(5))
                    .or(Cell.is_named("P3")));
        }
    }
}