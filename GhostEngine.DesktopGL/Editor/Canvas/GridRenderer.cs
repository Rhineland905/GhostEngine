using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.Settings;
using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostEngine.DesktopGL.Editor.Canvas
{
    internal static class GridRenderer
    {
        public const int CellSize = 32;

        public static void Draw(SpriteBatch spriteBatch, Texture2D pixel, Level level)
        {
            if (level == null)
            {
                return;
            }

            Color color = ButtonsEventsSettings.GridColor;
            int w = level.Width * CellSize;
            int h = level.Height * CellSize;

            for (int x = 0; x <= w; x += CellSize)
            {
                spriteBatch.Draw(pixel, new Rectangle(x, 0, 1, h), color);
            }

            for (int y = 0; y <= h; y += CellSize)
            {
                spriteBatch.Draw(pixel, new Rectangle(0, y, w, 1), color);
            }
        }
    }
}
