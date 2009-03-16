using Castle.Core;
using Castle.Windsor;
using developwithpassion.bdd.contexts;
using MoMoney.Testing.MetaData;
using MoMoney.Testing.spechelpers.contexts;
using MoMoney.Testing.spechelpers.core;
using MoMoney.windows.ui;

namespace MoMoney.Infrastructure.Container.Windsor
{
    [Concern(typeof (WindsorDependencyRegistry))]
    public class when_registering_a_singleton_component_with_the_windsor_container : concerns_for<IDependencyRegistry>
    {
        it should_return_the_same_instance_each_time_its_resolved =
            () => result.should_be_the_same_instance_as(sut.get_a<IBird>());

        it should_not_return_null = () => result.should_not_be_null();

        public override IDependencyRegistry create_sut()
        {
            return get_the.registry(null);
            //return new WindsorDependencyRegistry(new WindsorContainerFactory().create());
        }

        context c = () =>
                        {
                            var container = the_dependency<IWindsorContainer>();
                            var bird = new BlueBird();
                            container.is_told_to(x => x.Resolve<IBird>()).it_will_return(bird);
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