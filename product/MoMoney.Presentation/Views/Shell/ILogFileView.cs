using Gorilla.Commons.Utility.Core;
using MoMoney.Presentation.Views.Core;

namespace MoMoney.Presentation.Views.Shell
{
    public interface ILogFileView : IDockedContentView, ICallback<string>
    {
        void display(string file_path);
    }
}