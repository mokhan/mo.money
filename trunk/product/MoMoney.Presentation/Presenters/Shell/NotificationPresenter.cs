using System.Text;
using System.Windows.Forms;
using Gorilla.Commons.Utility.Extensions;
using MoMoney.Service.Application;

namespace MoMoney.Presentation.Presenters.Shell
{
    public class NotificationPresenter : INotification
    {
        public void notify(params NotificationMessage[] messages)
        {
            var builder = new StringBuilder();
            messages.each(x => builder.AppendLine(x));
            MessageBox.Show(builder.ToString(), "Ooops...", MessageBoxButtons.OK);
        }
    }
}