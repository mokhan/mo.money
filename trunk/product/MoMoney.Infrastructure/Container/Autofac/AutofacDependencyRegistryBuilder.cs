using System;
using Autofac;
using Autofac.Builder;
using Autofac.Modules;
using AutofacContrib.DynamicProxy2;
using Gorilla.Commons.Infrastructure.Container;
using Gorilla.Commons.Utility.Core;
using Gorilla.Commons.Utility.Extensions;
using MoMoney.Infrastructure.proxies;

namespace MoMoney.Infrastructure.Container.Autofac
{
    public class AutofacDependencyRegistryBuilder : IDependencyRegistration //, IBuilder<IContainer>
    {
        readonly ContainerBuilder builder;
        readonly Func<IContainer> container;

        public AutofacDependencyRegistryBuilder() : this(new ContainerBuilder())
        {
        }

        public AutofacDependencyRegistryBuilder(ContainerBuilder builder)
        {
            this.builder = builder;
            builder.RegisterModule(new ImplicitCollectionSupportModule());
            builder.RegisterModule(new StandardInterceptionModule());
            container = () => builder.Build();
            container = container.memorize();
        }

        public void singleton<Contract, Implementation>() where Implementation : Contract
        {
            builder.Register<Implementation>().As<Contract>().SingletonScoped();
        }

        public void singleton<Contract>(Func<Contract> instance_of_the_contract)
        {
            builder.Register(x => instance_of_the_contract()).As<Contract>().SingletonScoped();
        }

        public void transient<Contract, Implementation>() where Implementation : Contract
        {
            transient(typeof (Contract), typeof (Implementation));
        }

        public void transient(Type contract, Type implementation)
        {
            if (contract.is_a_generic_type())
                builder.RegisterGeneric(implementation).As(contract).FactoryScoped();
            else
                builder.Register(implementation).As(contract).FactoryScoped();
        }

        public void proxy<T>(IConfiguration<IProxyBuilder<T>> configuration, Func<T> target)
        {
            var proxy_builder = new ProxyBuilder<T>();
            configuration.configure(proxy_builder);
            builder.Register(x => proxy_builder.create_proxy_for(target)).As<T>().FactoryScoped();
        }

        public void proxy<T, Configuration>(Func<T> target)
            where Configuration : IConfiguration<IProxyBuilder<T>>, new()
        {
            proxy(new Configuration(), target);
        }

        public IDependencyRegistry build()
        {
            return new AutofacDependencyRegistry(container);
        }
    }
}