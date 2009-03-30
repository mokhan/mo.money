using MoMoney.Utility.Core;

namespace MoMoney.Tasks.infrastructure.core
{
    public interface IRunQueryCommand<T> : ICommand
    {
    }

    public class RunQueryCommand<T> : IRunQueryCommand<T>
    {
        readonly ICallback<T> callback;
        readonly IProcessQueryCommand<T> command;

        public RunQueryCommand(ICallback<T> callback, IProcessQueryCommand<T> command)
        {
            this.callback = callback;
            this.command = command;
        }

        public void run()
        {
            command.run(callback);
        }
    }
}