using Myra.Graphics2D.UI;
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
            layout.RowsProportions.Add(new Proportion(ProportionType.Fill));    // 1: ToolBar + ToolChoice + WorkSpace
            layout.RowsProportions.Add(new Proportion(ProportionType.Auto));    //End line
            layout.ColumnsProportions.Add(new Proportion(ProportionType.Auto)); // 0: ToolChoice (tool buttons) 
            layout.ColumnsProportions.Add(new Proportion(ProportionType.Auto)); // 1: ToolBar (fixed width)
            layout.ColumnsProportions.Add(new Proportion(ProportionType.Fill)); // 2: WorkSpace

            //Add MenuBar
            var menu = MenuBar.CreateMenuBar();
            Grid.SetRow(menu, 0);
            Grid.SetColumnSpan(menu, 3);
            layout.Widgets.Add(menu);

            //Add ToolBar
            var toolbar = Toolbar.CreateToolbar();
            Grid.SetRow(toolbar, 1);
            Grid.SetColumn(toolbar, 1);
            layout.Widgets.Add(toolbar);

            //Add ToolChoice
            var tools = ToolChoice.CreateToolPanel();
            Grid.SetRow(tools, 1);
            Grid.SetColumn(tools, 0);
            layout.Widgets.Add(tools);

            //Add WorkSpace
            var workSpace = WorkSpace.CreateWorkSpace();
            Grid.SetRow(workSpace, 1);
            Grid.SetColumn(workSpace, 2);
            layout.Widgets.Add(workSpace);

            //Botom Line
            var Line = BotomLine.Line();
            Grid.SetRow(Line, 2);
            Grid.SetColumnSpan(Line, 3);
            layout.Widgets.Add(Line);

            panel.Widgets.Add(layout);
            return panel;
        }

       
        
        
    }
}
