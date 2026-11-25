using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework.Input;
using System;

namespace GhostEngine.DesktopGL.Editor.Canvas
{
    internal class PlacementTool
    {
        public static void Update(MouseState current, MouseState previous, bool isActive)
        {
            if (!isActive) return;
            if(!WorkSpace.ScreenBounds.Contains(current.Position)) return;
            if (ButtonsEvents.SettingsWindowInstance != null) return;
            var level = EditorState.CurrentLevel;
            if (level == null) return;

            var area = WorkSpace.ScreenBounds;
            var world = Camera.ScreenToWorld(current.Position, area);
            int x = (int)System.Math.Floor(world.X / GridRenderer.CellSize);
            int y = (int)System.Math.Floor(world.Y / GridRenderer.CellSize);

            int wheelDelta = current.ScrollWheelValue - previous.ScrollWheelValue;


            if (current.LeftButton == ButtonState.Pressed && PaletteList.SelectedTile != null && ToolChoice.CheckToolState() == 2)
            {
                level.SetTile(x, y, PaletteList.SelectedTile);
            }
            // Objects are placed once per click, otherwise holding the button would add one every frame
            else if (JustPressed(current.LeftButton, previous.LeftButton) && PaletteList.SelectedObject != null && ToolChoice.CheckToolState() == 2)
            {
                level.PlaceObject(x, y, PaletteList.SelectedObject);
            }
            // Right button erases the object in the cell first, then the tile under it
            else if (current.LeftButton == ButtonState.Pressed && ToolChoice.CheckToolState() == 3)
            {
                if (!level.RemoveObjectAt(x, y))
                {
                    level.SetTile(x, y, null);
                }
            }
            else if (wheelDelta != 0)
            {
                Camera.ZoomAt(wheelDelta, current.Position, area);
            }
            else if (ToolChoice.CheckToolState() == 4 && current.LeftButton == ButtonState.Pressed || current.MiddleButton == ButtonState.Pressed) 
            {
                Camera.Offset -= (current.Position - previous.Position).ToVector2();
                return;
            }
        }

        private static bool JustPressed(ButtonState current, ButtonState previous)
        {
            return current == ButtonState.Pressed && previous == ButtonState.Released;
        }

    }
}
