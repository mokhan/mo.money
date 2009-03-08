using System;
using System.ComponentModel;
using Castle.Core.Interceptor;
using MoMoney.Utility.Extensions;

namespace MoMoney.Infrastructure.interceptors
{
    public interface ISynchronizedInterceptor : IInterceptor
    {
    }

    public class SynchronizedInterceptor<T> : ISynchronizedInterceptor where T : ISynchronizeInvoke
    {
        public void Intercept(IInvocation invocation)
        {
            var target = invocation.InvocationTarget.downcast_to<T>();
            target.BeginInvoke(do_it(invocation.Proceed), new object[] {});
        }

        Delegate do_it(Action action)
        {
            return action;
        }
    }
}