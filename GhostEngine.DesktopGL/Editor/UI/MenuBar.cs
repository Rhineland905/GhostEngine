using GhostEngine.DesktopGL.Editor.Settings;
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

            var file = new MenuItem { Text = Localization.Get("MenuBar.File") };
            AddItem(file, "New Project", "MenuBar.NewProject", "Ctrl+N");
            AddItem(file, "Open Project", "MenuBar.OpenProject", "Ctrl+P");
            AddItem(file, "New Level", "MenuBar.NewLevel", "");
            AddItem(file, "Open", "MenuBar.Open", "Ctrl+O");
            AddItem(file, "Save", "MenuBar.Save", "Ctrl+S");
            file.Items.Add(new MenuSeparator());
            AddItem(file, "Settings", "MenuBar.Settings", "");
            file.Items.Add(new MenuSeparator());
            AddItem(file, "Exit", "MenuBar.Exit", "Alt+F4");

            menu.Items.Add(file);
            return menu;
        }
        // command never changes (ButtonsEvents and Hotkeys switch on it); only the shown text is translated
        private static void AddItem(MenuItem parent, string command, string textKey, string shortcut)
        {
            var item = new MenuItem { Text = Localization.Get(textKey), ShortcutText = shortcut };
            item.Selected += (s, e) => ButtonsEvents.HandleMenuEvent(command);
            parent.Items.Add(item);
        }
    }
}
