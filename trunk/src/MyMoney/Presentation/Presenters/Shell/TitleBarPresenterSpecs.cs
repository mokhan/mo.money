using jpboodhoo.bdd.contexts;
using MyMoney.Infrastructure.eventing;
using MyMoney.Presentation.Model.messages;
using MyMoney.Presentation.Model.Projects;
using MyMoney.Presentation.Views.Shell;
using MyMoney.Testing.MetaData;
using MyMoney.Testing.spechelpers.contexts;
using MyMoney.Testing.spechelpers.core;

namespace MyMoney.Presentation.Presenters.Shell
{
    [Concern(typeof (TitleBarPresenter))]
    public class behaves_like_a_title_bar_presenter : concerns_for<ITitleBarPresenter, TitleBarPresenter>
    {
        public override ITitleBarPresenter create_sut()
        {
            return new TitleBarPresenter(view, project, broker);
        }

        context c = () =>
                        {
                            project = the_dependency<IProject>();
                            view = the_dependency<ITitleBar>();
                            broker = the_dependency<IEventAggregator>();
                        };

        protected static ITitleBar view;
        protected static IEventAggregator broker;
        protected static IProject project;
    }

    public class when_initializing_the_title_bar_for_the_first_time : behaves_like_a_title_bar_presenter
    {
        it should_display_the_name_of_the_file_that_is_opened = () => view.was_told_to(x => x.display("untitled.mo"));

        it should_ask_to_be_notified_of_any_unsaved_changes =
            () => broker.was_told_to(x => x.subscribe_to<unsaved_changes_event>(sut));

        it should_ask_to_be_notified_when_the_project_is_saved =
            () => broker.was_told_to(x => x.subscribe_to<saved_changes_event>(sut));

        it should_ask_to_be_notified_when_a_new_project_is_opened =
            () => broker.was_told_to(x => x.subscribe_to<new_project_opened>(sut));

        context c = () => when_the(project).is_told_to(x => x.name()).it_will_return("untitled.mo");

        because b = () => sut.run();
    }

    public class when_there_are_unsaved_changes_in_the_project : behaves_like_a_title_bar_presenter
    {
        it should_display_an_asterik_in_the_title = () => view.was_told_to(x => x.append_asterik());

        context c = () => { dto = new unsaved_changes_event(); };

        because b = () => sut.notify(dto);

        static unsaved_changes_event dto;
    }
}