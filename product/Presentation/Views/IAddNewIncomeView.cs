using System.Collections.Generic;
using Gorilla.Commons.Utility.Core;
using MoMoney.DTO;
using MoMoney.Presentation.Presenters;
using MoMoney.Presentation.Views.Core;

namespace MoMoney.Presentation.Views
{
    public interface IAddNewIncomeView : IDockedContentView,
                                         IView<IAddNewIncomePresenter>,
                                         ICallback<IEnumerable<CompanyDTO>>,
                                         ICallback<IEnumerable<IncomeInformationDTO>>
    {
    }
}