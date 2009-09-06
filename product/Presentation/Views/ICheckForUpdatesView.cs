using Gorilla.Commons.Utility;
using Gorilla.Commons.Utility.Core;
using MoMoney.DTO;
using MoMoney.Presentation.Presenters.updates;
using MoMoney.Presentation.Views.Core;

namespace MoMoney.Presentation.Views.updates
{
    public interface ICheckForUpdatesView : IView<ICheckForUpdatesPresenter>, ICallback<ApplicationVersion>
    {
        void display();
        void downloaded(Percent percentage_complete);
        void update_complete();
        void close();
    }
}