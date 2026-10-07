using Myra.Graphics2D.UI;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using GhostEngine.DesktopGL.Editor.UI;

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

            var menu = MenuBar.CreateMenuBar();
            Grid.SetRow(menu, 0);
            layout.Widgets.Add(menu);

            var toolbar = Toolbar.CreateToolbar();
            Grid.SetRow(toolbar, 1);
            layout.Widgets.Add(toolbar);

            panel.Widgets.Add(layout);
            return panel;
        }

       
        
        
    }
}
