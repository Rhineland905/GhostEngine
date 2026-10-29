using GhostEngine.DesktopGL.Editor.SaveLoadData;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor
{
    internal class Hotkeys
    {
        private static KeyboardState _current;
        private static KeyboardState _previous;

        public static void Update(bool isActive, Desktop desktop)
        {
            _previous = _current;
            _current = Keyboard.GetState();

            if (!isActive || desktop == null || desktop.HasModalWidget) { return; }
            bool ctrl = _current.IsKeyDown(Keys.LeftControl) || _current.IsKeyDown(Keys.RightControl);
            if (ctrl && JustPressed(Keys.S))
            {
                if (!(ButtonsEvents.PathLastSaveLevel == null))
                {
                    SaveLevel.Save(EditorState.CurrentLevel, ButtonsEvents.PathLastSaveLevel);
                }
                else
                {
                    ButtonsEvents.HandleMenuEvent("Save");
                }

            }
            else if (ctrl && JustPressed(Keys.O))
            {
                ButtonsEvents.HandleMenuEvent("Open");
            }
            else if (ctrl && JustPressed(Keys.N))
            {
                ButtonsEvents.HandleMenuEvent("New Project");
            }
            else if (ctrl && JustPressed(Keys.P)) {
                ButtonsEvents.HandleMenuEvent("Open Project");
            }
            if (desktop.FocusedKeyboardWidget is TextBox)
            {
                return;
            }

        }
        private static bool JustPressed(Keys key) { 
            return _current.IsKeyDown(key) && _previous.IsKeyUp(key);
        }
        
    }
}
