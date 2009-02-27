using System;
using MyMoney.Utility.Extensions;

namespace MyMoney.Presentation.Views.Shell
{
    public interface ITaskTrayMessageView
    {
        void display(string message, params object[] arguments);
    }

    public class TaskTrayMessage : ITaskTrayMessageView, IDisposable
    {
        readonly INotificationIconView view;

        public TaskTrayMessage(INotificationIconView view)
        {
            this.view = view;
        }

        public void display(string message, params object[] arguments)
        {
            view.show_popup_message(message.formatted_using(arguments));
        }

        public void Dispose()
        {
        }
    }
}