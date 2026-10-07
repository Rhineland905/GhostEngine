using Myra.Graphics2D.UI;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;

namespace GhostEngine.DesktopGL.Editor
{
    public class UIManager
    {
        public static Panel CreateMainUI()
        {
            var panel = new Panel();

            var layout = new Grid();
            layout.RowsProportions.Add(new Proportion(ProportionType.Auto)); // 0: Menu
            layout.RowsProportions.Add(new Proportion(ProportionType.Auto)); // 1: ToolBar
            layout.RowsProportions.Add(new Proportion(ProportionType.Fill)); // 2: WorkSpace

            var menu = CreateMenuBar();
            Grid.SetRow(menu, 0);
            layout.Widgets.Add(menu);

            var toolbar = CreateToolbar();
            Grid.SetRow(toolbar, 1);
            layout.Widgets.Add(toolbar);

            panel.Widgets.Add(layout);
            return panel;
        }

        private static HorizontalMenu CreateMenuBar()
        {
            var menu = new HorizontalMenu
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            var file = new MenuItem { Text = "File" };
            AddItem(file, "New", "Ctrl+N");
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
        private static VerticalStackPanel CreateToolbar()
        {
            var toolBar = new VerticalStackPanel()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                

            };
            var test = new Button
            {
                Content = new Label { Text = "Apply", TextColor = Color.White },

            };
            toolBar.Widgets.Add(test);
            return toolBar;
        }
        
    }
}
