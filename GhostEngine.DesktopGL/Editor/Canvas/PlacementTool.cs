using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework.Input;

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
            int x = (current.X - area.X) / GridRenderer.CellSize;
            int y = (current.Y - area.Y) / GridRenderer.CellSize;

            if (current.LeftButton == ButtonState.Pressed && PaletteList.SelectedTile != null)
            {
                level.SetTile(x, y, PaletteList.SelectedTile);
            }
            // Objects are placed once per click, otherwise holding the button would add one every frame
            else if (JustPressed(current.LeftButton, previous.LeftButton) && PaletteList.SelectedObject != null)
            {
                level.PlaceObject(x, y, PaletteList.SelectedObject);
            }
            // Right button erases the object in the cell first, then the tile under it
            else if (current.RightButton == ButtonState.Pressed)
            {
                if (!level.RemoveObjectAt(x, y))
                {
                    level.SetTile(x, y, null);
                }
            }
        }

        private static bool JustPressed(ButtonState current, ButtonState previous)
        {
            return current == ButtonState.Pressed && previous == ButtonState.Released;
        }

    }
}
