using System;
using System.Collections.Generic;
using MoMoney.Infrastructure.Threading;
using MoMoney.Utility.Core;

namespace MoMoney.Utility.Extensions
{
    public static class CommandExtensions
    {
        public static ICommand then<Command>(this ICommand left) where Command : ICommand, new()
        {
            return then(left, new Command());
        }

        public static ICommand then(this ICommand left, ICommand right)
        {
            return new ChainedCommand(left, right);
        }

        public static ICommand then(this ICommand left, Action right)
        {
            return new ChainedCommand(left, new ActionCommand(right));
        }

        public static ICommand as_command_chain(this IEnumerable<ICommand> commands)
        {
            var processor = new CommandProcessor();
            commands.each(processor.add);
            return processor;
        }
    }
}