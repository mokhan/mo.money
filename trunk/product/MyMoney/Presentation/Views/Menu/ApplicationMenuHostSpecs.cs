using System.Windows.Forms;
using jpboodhoo.bdd.contexts;
using MyMoney.Presentation.Model.Menu;
using MyMoney.Presentation.Views.Menu.Mappers;
using MyMoney.Presentation.Views.Shell;
using MyMoney.Testing.MetaData;
using MyMoney.Testing.spechelpers.contexts;
using MyMoney.Testing.spechelpers.core;

namespace MyMoney.Presentation.Views.Menu
{
    [Concern(typeof (ApplicationMenuHost))]
    public class behaves_like_application_menu_host : concerns_for<IMenuView, ApplicationMenuHost>
    {
        context c = () =>
                        {
                            mapper = the_dependency<ISubMenuToToolStripMenuItemMapper>();
                            main_shell = the_dependency<IShell>();
                        };

        static protected ISubMenuToToolStripMenuItemMapper mapper;
        static protected IShell main_shell;
    }

    public class when_adding_sub_menus_to_the_main_menu : behaves_like_application_menu_host
    {
        it should_add_the_mapped_menu_strip_item_to_the_main_menu_strip =
            () => main_shell.was_told_to(x => x.add_to_main_menu(tool_strip_menu_item));

        context c = () =>
                        {
                            mapper = the_dependency<ISubMenuToToolStripMenuItemMapper>();
                            main_shell = the_dependency<IShell>();
                            sub_menu = an<ISubMenu>();
                            tool_strip_menu_item = new ToolStripMenuItem();
                            mapper.is_told_to(x => x.map_from(sub_menu)).it_will_return(tool_strip_menu_item);
                        };

        because b = () => sut.add(sub_menu);

        static ISubMenu sub_menu;
        static ToolStripMenuItem tool_strip_menu_item;
    }
}