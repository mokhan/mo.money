using MoMoney.Presentation.Views;

namespace MoMoney.Presentation.Core
{
    public interface IContentPresenter : IPresenter
    {
        IDockedContentView View { get; }
        void activate();
        void deactivate();
        bool can_close();
    }
}