using Microsoft.Xna.Framework;
using System;

namespace GhostEngine.DesktopGL.Editor.Canvas
{
    internal static class Camera
    {
        public static Vector2 Offset;
        public static float Zoom = 1f;

        private const float MinZoom = 0.75f;
        private const float MaxZoom = 4f;

        public static void ZoomAt(int wheelDelta, Point mouse, Rectangle area)
        {
            var before = ScreenToWorld(mouse, area);
         
            Zoom *= wheelDelta > 0 ? 1.1f : 1f / 1.1f;
            Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);
            Offset = before * Zoom - (mouse - area.Location).ToVector2();
        }

        public static Matrix GetTransform(Rectangle area)
        {
            return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(area.X - Offset.X, area.Y - Offset.Y, 0);
        }

        public static Vector2 ScreenToWorld(Point mouse, Rectangle area)
        {
            return (mouse.ToVector2() - area.Location.ToVector2() + Offset) / Zoom;
        }
    }
}
