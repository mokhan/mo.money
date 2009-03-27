using System;
using MoMoney.Infrastructure.proxies;
using MoMoney.Utility.Core;

namespace MoMoney.Infrastructure.Container
{
    public interface IDependencyRegistration
    {
        void singleton<Contract, Implementation>() where Implementation : Contract;
        void singleton<Contract>(Func<Contract> instance_of_the_contract);
        void transient<Contract, Implementation>() where Implementation : Contract;
        void transient(Type contract, Type implementation);
        void proxy<T>(IConfiguration<IProxyBuilder<T>> configuration, Func<T> target);
    }
}