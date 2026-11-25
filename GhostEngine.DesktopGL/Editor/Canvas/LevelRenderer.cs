using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.Model.Objects;
using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostEngine.DesktopGL.Editor.Canvas
{
    internal static class LevelRenderer
    {
        public static void Draw(SpriteBatch spriteBatch, Texture2D pixel, Level level)
        {
            if (level == null)
            {
                return;
            }

            int size = GridRenderer.CellSize;

            for (int y = 0; y < level.Height; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    TileDefinition tile = level.GetTile(x, y);
                    if (tile == null)
                    {
                        continue;
                    }

                    var cell = new Rectangle(x * size, y * size, size, size);
                    spriteBatch.Draw(pixel, cell, tile.PreviewColor);
                }
            }
        }
    
    

        // Objects go on top of tiles and the grid; drawn smaller than a cell so they stand out from tiles
        public static void DrawObjects(SpriteBatch spriteBatch, Texture2D pixel, Level level)
        {
            Rectangle area = WorkSpace.ScreenBounds;

            if (level == null)
            {
                return;
            }

            int size = GridRenderer.CellSize;
            int inset = size / 5;

            foreach (GameObject gameObject in level.Objects)
            {
                var cell = new Rectangle(gameObject.Cell.X * size, gameObject.Cell.Y * size, size, size);
                

                cell.Inflate(-inset, -inset);
                spriteBatch.Draw(pixel, cell, gameObject.Definition.PreviewColor);
            }
        }
    }
}
