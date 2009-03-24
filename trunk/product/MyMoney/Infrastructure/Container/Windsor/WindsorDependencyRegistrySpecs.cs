using Castle.Core;
using Castle.Windsor;
using developwithpassion.bdd.contexts;
using MoMoney.Testing.MetaData;
using MoMoney.Testing.spechelpers.contexts;
using MoMoney.Testing.spechelpers.core;

namespace MoMoney.Infrastructure.Container.Windsor
{
    [Concern(typeof (WindsorDependencyRegistry))]
    internal class behaves_like_windsor_dependency_registry :
        concerns_for<IDependencyRegistry, WindsorDependencyRegistry>
    {
    }

    internal class when_registering_a_singleton_component_with_the_windsor_container :
        behaves_like_windsor_dependency_registry
    {
        it should_return_the_same_instance_each_time_its_resolved =
            () => result.should_be_the_same_instance_as(sut.get_a<IBird>());

        it should_not_return_null = () => result.should_not_be_null();

        context c = () =>
                        {
                            var container = the_dependency<IWindsorContainer>();
                            var bird = new BlueBird();
                            container.is_told_to(x => x.Resolve<IBird>()).it_will_return(bird).Repeat.Any();
                        };

        because b = () => { result = sut.get_a<IBird>(); };


        static IBird result;
    }

    [Singleton]
    public class BlueBird : IBird
    {
    }

    public interface IBird
    {
    }
}