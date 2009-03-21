using MoMoney.Infrastructure.eventing;
using MoMoney.Presentation.Core;
using MoMoney.Presentation.Model.messages;
using MoMoney.Presentation.Model.Projects;
using MoMoney.Presentation.Views.Shell;

namespace MoMoney.Presentation.Presenters.Shell
{
    public interface ITitleBarPresenter : IPresentationModule,
                                          IEventSubscriber<unsaved_changes_event>,
                                          IEventSubscriber<saved_changes_event>,
                                          IEventSubscriber<new_project_opened>
    {
    }

    public class TitleBarPresenter : ITitleBarPresenter
    {
        readonly ITitleBar view;
        readonly IProject project;
        readonly IEventAggregator broker;

        public TitleBarPresenter(ITitleBar view, IProject project, IEventAggregator broker)
        {
            this.view = view;
            this.project = project;
            this.broker = broker;
        }

        public void run()
        {
            view.display(project.name());
            broker.subscribe_to<unsaved_changes_event>(this);
            broker.subscribe_to<saved_changes_event>(this);
            broker.subscribe_to<new_project_opened>(this);
        }

        public void notify(unsaved_changes_event dto)
        {
            view.append_asterik();
        }

        public void notify(saved_changes_event message)
        {
            view.display(project.name());
            view.remove_asterik();
        }

        public void notify(new_project_opened message)
        {
            view.display(project.name());
        }
    }
}