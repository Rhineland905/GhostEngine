using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;


namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class Objects
    {
        public static VerticalStackPanel CreateTabObjects()
        {
            var ObjectsTab = PaletteList.CreateList();

            ObjectsTab.Widgets.Add(PaletteList.CreateHeader("Player", first: true));
            ObjectsTab.Widgets.Add(PaletteList.CreateItem("Player Spawn", new Color(0, 128, 255)));

            ObjectsTab.Widgets.Add(PaletteList.CreateHeader("Enemies"));
            ObjectsTab.Widgets.Add(PaletteList.CreateItem("Slime", new Color(255, 0, 0)));
            ObjectsTab.Widgets.Add(PaletteList.CreateItem("Bat", new Color(255, 153, 51)));

            ObjectsTab.Widgets.Add(PaletteList.CreateHeader("Items"));
            ObjectsTab.Widgets.Add(PaletteList.CreateItem("Coin", new Color(255, 255, 0)));


            ObjectsTab.Widgets.Add(PaletteList.CreateHeader("Physics"));

            ObjectsTab.Widgets.Add(PaletteList.CreateHeader("Triggers"));

            return ObjectsTab;
        }
    }
}
