using System.Windows.Forms;
using Gorilla.Commons.Utility.Extensions;
using MoMoney.Presentation.Model.Navigation;
using MoMoney.Presentation.Resources;
using MoMoney.Presentation.Views.core;
using MoMoney.Presentation.Views.Shell;
using WeifenLuo.WinFormsUI.Docking;

namespace MoMoney.Presentation.Views.Navigation
{
    public partial class NavigationView : ApplicationDockedWindow, INavigationView
    {
        readonly IShell shell;

        public NavigationView(IShell shell)
        {
            InitializeComponent();
            this.shell = shell;
            icon(ApplicationIcons.FileExplorer).docked_to(DockState.DockRightAutoHide);
            uxNavigationTreeView.ImageList = new ImageList();
            ApplicationIcons.all().each(x => uxNavigationTreeView.ImageList.Images.Add(x.name_of_the_icon, x));
        }

        public void accept(INavigationTreeVisitor tree_view_visitor)
        {
            uxNavigationTreeView.Nodes.Clear();
            tree_view_visitor.visit(uxNavigationTreeView);
            shell.add(this);
        }
    }
}