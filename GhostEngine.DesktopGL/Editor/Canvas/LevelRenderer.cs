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
            Rectangle area = WorkSpace.ScreenBounds;

            if (level == null || area.Width <= 0 || area.Height <= 0)
            {
                return;
            }

            int size = GridRenderer.CellSize;

            int visibleWidth = System.Math.Min(level.Width, (area.Width + size - 1) / size);
            int visibleHeight = System.Math.Min(level.Height, (area.Height + size - 1) / size);

            for (int y = 0; y < visibleHeight; y++)
            {
                for (int x = 0; x < visibleWidth; x++)
                {
                    TileDefinition tile = level.GetTile(x, y);
                    if (tile == null)
                    {
                        continue;
                    }

                    var cell = new Rectangle(area.X + x * size, area.Y + y * size, size, size);
                    spriteBatch.Draw(pixel, cell, tile.PreviewColor);
                }
            }
        }

        // Objects go on top of tiles and the grid; drawn smaller than a cell so they stand out from tiles
        public static void DrawObjects(SpriteBatch spriteBatch, Texture2D pixel, Level level)
        {
            Rectangle area = WorkSpace.ScreenBounds;

            if (level == null || area.Width <= 0 || area.Height <= 0)
            {
                return;
            }

            int size = GridRenderer.CellSize;
            int inset = size / 5;

            foreach (GameObject gameObject in level.Objects)
            {
                var cell = new Rectangle(area.X + gameObject.Cell.X * size, area.Y + gameObject.Cell.Y * size, size, size);
                if (!area.Intersects(cell))
                {
                    continue;
                }

                cell.Inflate(-inset, -inset);
                spriteBatch.Draw(pixel, cell, gameObject.Definition.PreviewColor);
            }
        }
    }
}
