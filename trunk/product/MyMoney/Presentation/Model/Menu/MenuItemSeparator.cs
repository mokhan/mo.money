using System.Windows.Forms;

namespace MoMoney.Presentation.Model.Menu
{
    internal class MenuItemSeparator : IMenuItem
    {
        public string name { get; private set; }

        public void click()
        {
        }

        public ToolStripItem build()
        {
            return new ToolStripSeparator();
        }

        public System.Windows.Forms.MenuItem build_menu_item()
        {
            return new System.Windows.Forms.MenuItem("----------");
        }
    }
}