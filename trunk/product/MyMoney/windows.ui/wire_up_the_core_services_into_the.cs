using MoMoney.Infrastructure.Container;
using MoMoney.Infrastructure.Container.Windsor;
using MoMoney.Utility.Core;

namespace MoMoney.windows.ui
{
    internal class wire_up_the_core_services_into_the : ICommand
    {
        private readonly WindsorDependencyRegistry registry;

        public wire_up_the_core_services_into_the(WindsorDependencyRegistry registry)
        {
            this.registry = registry;
        }

        public void run()
        {
            resolve.initialize_with(registry);
        }
    }
}