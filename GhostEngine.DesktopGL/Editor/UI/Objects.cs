using GhostEngine.DesktopGL.Editor.Model.Objects;
using Myra.Graphics2D.UI;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class Objects
    {
        private static readonly string[] Categories = { "Player", "Enemies", "Items", "Physics", "Triggers" };

        public static VerticalStackPanel CreateTabObjects()
        {
            var ObjectsTab = PaletteList.CreateList();

            for (int i = 0; i < Categories.Length; i++)
            {
                ObjectsTab.Widgets.Add(PaletteList.CreateHeader(Categories[i], first: i == 0));

                foreach (var definition in ObjectCatalog.All)
                {
                    if (definition.Category == Categories[i])
                    {
                        ObjectsTab.Widgets.Add(PaletteList.CreateObjectItem(definition));
                    }
                }
            }

            return ObjectsTab;
        }
    }
}
