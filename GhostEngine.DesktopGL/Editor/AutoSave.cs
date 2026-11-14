using GhostEngine.DesktopGL.Editor.SaveLoadData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor
{
    internal class AutoSave
    {
        public static void autoSave(ref double _autoSaveTimer, GameTime gameTime)
        {
            if (ButtonsEvents.PathLastSaveLevel == null) return;
            try { 
                bool IsDirty = EditorState.CurrentLevel.IsDirty;
                if (IsDirty)
                {
                    _autoSaveTimer += gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (_autoSaveTimer >= 300)
                {
                    _autoSaveTimer = 0;
                    SaveLevel.Save(EditorState.CurrentLevel, ButtonsEvents.PathLastSaveLevel);
                    EditorState.CurrentLevel.IsDirty = false;
                }
            }
            catch { return; }
        }
    }
}
