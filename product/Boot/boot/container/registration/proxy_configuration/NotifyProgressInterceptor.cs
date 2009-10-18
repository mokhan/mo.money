using Castle.Core.Interceptor;
using MoMoney.Presentation.Model.messages;
using MoMoney.Service.Infrastructure.Eventing;

namespace MoMoney.boot.container.registration.proxy_configuration
{
    public interface INotifyProgressInterceptor : IInterceptor {}

    public class NotifyProgressInterceptor : INotifyProgressInterceptor
    {
        readonly IEventAggregator broker;

        public NotifyProgressInterceptor(IEventAggregator broker)
        {
            this.broker = broker;
        }

        public void Intercept(IInvocation invocation)
        {
            broker.publish(new StartedRunningCommand(invocation.InvocationTarget));
            invocation.Proceed();
            broker.publish(new FinishedRunningCommand(invocation.InvocationTarget));
        }
    }
}