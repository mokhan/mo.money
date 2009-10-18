using developwithpassion.bdd.contexts;
using Gorilla.Commons.Testing;
using MoMoney.Presentation.Views;
using MoMoney.Presentation.Winforms.Resources;
using MoMoney.Service.Infrastructure.Eventing;

namespace MoMoney.Presentation.Presenters
{
    [Concern(typeof (NotificationIconPresenter))]
    public abstract class behaves_like_notification_icon_presenter : concerns_for<INotificationIconPresenter, NotificationIconPresenter>
    {
        //public override INotificationIconPresenter create_sut()
        //{
        //    return new NotificationIconPresenter(view, broker);
        //}

        context c = () =>
        {
            view = the_dependency<INotificationIconView>();
            broker = the_dependency<IEventAggregator>();
        };

        protected static INotificationIconView view;
        protected static IEventAggregator broker;
    }

    public class when_initializing_the_notification_icon : behaves_like_notification_icon_presenter
    {
        it should_ask_the_view_to_display_the_correct_icon_and_text = () => view.was_told_to(v => v.display(ApplicationIcons.Application, "mokhan.ca"));

        because b = () => sut.run();
    }
}