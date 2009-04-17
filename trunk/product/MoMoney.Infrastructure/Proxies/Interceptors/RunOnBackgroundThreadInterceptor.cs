using Castle.Core.Interceptor;
using MoMoney.Infrastructure.Threading;
using MoMoney.Utility.Core;

namespace MoMoney.Infrastructure.proxies.Interceptors
{
    public class RunOnBackgroundThreadInterceptor<CommandToExecute> : IInterceptor
        where CommandToExecute : IDisposableCommand
    {
        readonly IBackgroundThreadFactory thread_factory;

        public RunOnBackgroundThreadInterceptor(IBackgroundThreadFactory thread_factory)
        {
            this.thread_factory = thread_factory;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            using (thread_factory.create_for<CommandToExecute>())
            {
                invocation.Proceed();
            }
        }
    }
}