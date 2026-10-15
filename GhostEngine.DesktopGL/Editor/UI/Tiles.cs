using GhostEngine.DesktopGL.Editor.Model;
using Myra.Graphics2D.UI;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class Tiles
    {
        private static readonly string[] Categories = { "Terrain", "Platforms", "Hazards", "Decor" };

        public static VerticalStackPanel CreateTabTiles()
        {
            var TilessTab = PaletteList.CreateList();

            for (int i = 0; i < Categories.Length; i++)
            {
                TilessTab.Widgets.Add(PaletteList.CreateHeader(Categories[i], first: i == 0));

                foreach (var tile in TileCatalog.All)
                {
                    if (tile.Category == Categories[i])
                    {
                        TilessTab.Widgets.Add(PaletteList.CreateTileItem(tile));
                    }
                }
            }

            return TilessTab;
        }
    }
}
