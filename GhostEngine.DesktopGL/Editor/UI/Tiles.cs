using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class Tiles
    {
        public static VerticalStackPanel CreateTabTiles()
        {
            var TilessTab = PaletteList.CreateList();

            TilessTab.Widgets.Add(PaletteList.CreateHeader("Terrain", first: true));
            TilessTab.Widgets.Add(PaletteList.CreateItem("Floor", new Color(120, 130, 146)));

            TilessTab.Widgets.Add(PaletteList.CreateHeader("Platforms"));

            TilessTab.Widgets.Add(PaletteList.CreateHeader("Hazards"));

            TilessTab.Widgets.Add(PaletteList.CreateHeader("Decor"));

            return TilessTab;
        }
    }
}
