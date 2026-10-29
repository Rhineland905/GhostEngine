using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class MenuBar
    {
        public static HorizontalMenu CreateMenuBar()
        {
            var menu = new HorizontalMenu
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            var file = new MenuItem { Text = "File" };
            AddItem(file, "New Project", "Ctrl+N");
            AddItem(file, "Open Project", "Ctrl+P");
            AddItem(file, "New Level", "");
            AddItem(file, "Open", "Ctrl+O");
            AddItem(file, "Save", "Ctrl+S");
            file.Items.Add(new MenuSeparator());
            AddItem(file, "Settings", "");
            file.Items.Add(new MenuSeparator());
            AddItem(file, "Exit", "Alt+F4");

            menu.Items.Add(file);
            return menu;
        }
        private static void AddItem(MenuItem parent, string name, string shortcut)
        {
            var item = new MenuItem { Text = name, ShortcutText = shortcut };
            item.Selected += (s, e) => ButtonsEvents.HandleMenuEvent(name);
            parent.Items.Add(item);
        }
    }
}
