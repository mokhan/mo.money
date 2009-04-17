using MoMoney.Presentation.Views.Shell;
using MoMoney.Utility.Core;
using MoMoney.Utility.Extensions;

namespace MoMoney.Presentation.Core
{
    public interface IApplicationController
    {
        void run<Presenter>() where Presenter : IPresenter;
    }

    public class ApplicationController : IApplicationController, IParameterizedCommand<IPresenter>
    {
        readonly IPresenterRegistry registered_presenters;
        readonly IShell shell;

        public ApplicationController(IPresenterRegistry registered_presenters, IShell shell)
        {
            this.registered_presenters = registered_presenters;
            this.shell = shell;
        }

        public void run<Presenter>() where Presenter : IPresenter
        {
            run(registered_presenters.find_an_implementation_of<IPresenter, Presenter>());
        }

        public void run(IPresenter presenter)
        {
            presenter.run();
            if (presenter.is_an_implementation_of<IContentPresenter>())
            {
                var content_presenter = presenter.downcast_to<IContentPresenter>();
                var view = content_presenter.View;

                //view.on_activated = x => content_presenter.activate();
                //view.on_deactivate = x => content_presenter.deactivate();
                //view.on_closing = x => x.Cancel = !content_presenter.can_close();
                //view.on_closed = x => remove(presenter);

                shell.add(view);
            }
        }
    }
}