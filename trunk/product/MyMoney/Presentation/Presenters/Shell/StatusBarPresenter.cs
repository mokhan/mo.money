using MoMoney.Domain.Core;
using MoMoney.Infrastructure.eventing;
using MoMoney.Presentation.Core;
using MoMoney.Presentation.Model.messages;
using MoMoney.Presentation.Resources;
using MoMoney.Presentation.Views.Shell;
using MoMoney.Utility.Extensions;

namespace MoMoney.Presentation.Presenters.Shell
{
    public interface IStatusBarPresenter : IPresentationModule,
                                           IEventSubscriber<SavedChangesEvent>,
                                           IEventSubscriber<NewProjectOpened>,
                                           IEventSubscriber<ClosingTheApplication>,
                                           IEventSubscriber<ClosingProjectEvent>
    {
    }

    public class StatusBarPresenter : IStatusBarPresenter
    {
        readonly IStatusBarView view;
        readonly IEventAggregator broker;

        public StatusBarPresenter(IStatusBarView view, IEventAggregator broker)
        {
            this.view = view;
            this.broker = broker;
        }

        public void run()
        {
            broker.subscribe_to<SavedChangesEvent>(this);
            broker.subscribe_to<NewProjectOpened>(this);
            broker.subscribe_to<ClosingTheApplication>(this);
            broker.subscribe_to<ClosingProjectEvent>(this);
        }

        public void notify(SavedChangesEvent message)
        {
            view.display(ApplicationIcons.ApplicationReady, "Last Saved: {0}".formatted_using(Clock.now()));
        }

        public void notify(NewProjectOpened message)
        {
            view.display(ApplicationIcons.ApplicationReady, "Ready");
        }

        public void notify(ClosingTheApplication message)
        {
            view.display(ApplicationIcons.Empty, "Good Bye!");
        }

        public void notify(ClosingProjectEvent message)
        {
            view.display(ApplicationIcons.ApplicationReady, "Ready");
        }
    }
}