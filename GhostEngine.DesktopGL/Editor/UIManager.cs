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
            var panel = new Panel { Background = null };

            // Fill row/column take whatever the window leaves, so the layout follows window resizes
            var layout = new Grid();
            layout.RowsProportions.Add(new Proportion(ProportionType.Auto));    // 0: Menu
            layout.RowsProportions.Add(new Proportion(ProportionType.Fill));    // 1: ToolBar + WorkSpace
            layout.ColumnsProportions.Add(new Proportion(ProportionType.Auto)); // 0: ToolBar (fixed width)
            layout.ColumnsProportions.Add(new Proportion(ProportionType.Fill)); // 1: WorkSpace

            var menu = MenuBar.CreateMenuBar();
            Grid.SetRow(menu, 0);
            Grid.SetColumnSpan(menu, 2);
            layout.Widgets.Add(menu);

            var toolbar = Toolbar.CreateToolbar();
            Grid.SetRow(toolbar, 1);
            Grid.SetColumn(toolbar, 0);
            layout.Widgets.Add(toolbar);

            var workSpace = WorkSpace.CreateWorkSpace();
            Grid.SetRow(workSpace, 1);
            Grid.SetColumn(workSpace, 1);
            layout.Widgets.Add(workSpace);

            panel.Widgets.Add(layout);
            return panel;
        }

       
        
        
    }
}
