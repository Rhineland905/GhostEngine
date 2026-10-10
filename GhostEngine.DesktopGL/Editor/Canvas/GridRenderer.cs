using GhostEngine.DesktopGL.Editor.Settings;
using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostEngine.DesktopGL.Editor.Canvas
{
    internal static class GridRenderer
    {
        public const int CellSize = 32;

        public static void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            Rectangle area = WorkSpace.ScreenBounds;

            if (area.Width <= 0 || area.Height <= 0)
            {
                return;
            }

            Color color = ButtonsEventsSettings.GridColor;

            for (int x = area.Left; x < area.Right; x += CellSize)
            {
                spriteBatch.Draw(pixel, new Rectangle(x, area.Top, 1, area.Height), color);
            }

            for (int y = area.Top; y < area.Bottom; y += CellSize)
            {
                spriteBatch.Draw(pixel, new Rectangle(area.Left, y, area.Width, 1), color);
            }
        }
    }
}
