using Gorilla.Commons.Infrastructure.Eventing;
using MoMoney.Presentation.Model.Menu;
using MoMoney.Presentation.Model.messages;
using MoMoney.Presentation.Presenters;
using MoMoney.Presentation.Presenters.Menu;

namespace MoMoney.Modules
{
    public class ApplicationMenuModule : IApplicationMenuModule
    {
        readonly IEventAggregator broker;
        readonly IRunPresenterCommand command;

        public ApplicationMenuModule(IEventAggregator broker, IRunPresenterCommand command)
        {
            this.broker = broker;
            this.command = command;
        }

        public void run()
        {
            broker.subscribe(this);
            command.run<IApplicationMenuPresenter>();
        }

        public void notify(NewProjectOpened message)
        {
            broker.publish<IMenuItem>(x => x.refresh());
        }

        public void notify(ClosingProjectEvent message)
        {
            broker.publish<IMenuItem>(x => x.refresh());
        }

        public void notify(SavedChangesEvent message)
        {
            broker.publish<IMenuItem>(x => x.refresh());
        }

        public void notify(UnsavedChangesEvent message)
        {
            broker.publish<IMenuItem>(x => x.refresh());
        }
    }
}