using System.Collections.Generic;
using System.Windows.Forms;
using MyMoney.Presentation.Core;
using MyMoney.Presentation.Model.Menu;
using MyMoney.Presentation.Model.Menu.File.Commands;
using MyMoney.Presentation.Resources;
using MyMoney.Presentation.Views.Shell;
using MyMoney.Utility.Extensions;

namespace MyMoney.Presentation.Presenters.Shell
{
    public interface IToolbarPresenter : IPresentationModule
    {
    }

    public class ToolBarPresenter : IToolbarPresenter
    {
        readonly IShell shell;

        public ToolBarPresenter(IShell shell)
        {
            this.shell = shell;
        }

        public void run()
        {
            all_tool_bar_buttons().each(x => shell.add_to_tool_bar(x));
        }

        IEnumerable<ToolStripItem> all_tool_bar_buttons()
        {
            yield return create
                .a_tool_bar_item()
                .with_tool_tip_text_as("New")
                .when_clicked_executes<INewCommand>()
                .displays_icon(ApplicationIcons.NewProject)
                .build();
            yield return create
                .a_tool_bar_item()
                .with_tool_tip_text_as("Open")
                .when_clicked_executes<IOpenCommand>()
                .displays_icon(ApplicationIcons.OpenProject)
                .build();
            yield return create
                .a_tool_bar_item()
                .with_tool_tip_text_as("Save")
                .when_clicked_executes<ISaveCommand>()
                .displays_icon(ApplicationIcons.SaveProject)
                .build();
        }
    }
}