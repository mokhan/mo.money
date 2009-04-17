using Gorilla.Commons.Utility.Core;
using MoMoney.Infrastructure.eventing;
using MoMoney.Infrastructure.System;
using MoMoney.Presentation.Model.messages;

namespace MoMoney.Presentation.Presenters.Commands
{
    public interface IRestartCommand : ICommand
    {
    }

    public class RestartCommand : IRestartCommand
    {
        readonly IApplication application;
        readonly IEventAggregator broker;

        public RestartCommand(IApplication application, IEventAggregator broker)
        {
            this.application = application;
            this.broker = broker;
        }

        public void run()
        {
            broker.publish<ClosingTheApplication>();
            application.restart();
        }
    }
}