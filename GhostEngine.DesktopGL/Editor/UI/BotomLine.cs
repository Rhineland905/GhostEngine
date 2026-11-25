using GhostEngine.DesktopGL.Editor.Settings;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class BotomLine
    {
        private static Image _savedIcon;
        private static Image _unsavedIcon;
        private static Label _LevelName;

        public static HorizontalStackPanel Line()
        {
            var pathS = Path.Combine(AppContext.BaseDirectory, "Content", "Status", "saved.png");
            var pathU = Path.Combine(AppContext.BaseDirectory, "Content", "Status", "unsaved.png");
            var line = new HorizontalStackPanel
            {
                Height = 20,
                Background = new SolidBrush(ButtonsEventsSettings.SurfaceColor),
                Border = new SolidBrush(ButtonsEventsSettings.BorderColor),
                BorderThickness = new Thickness(0, 1, 0, 0)
            };
            _LevelName = new Label
            {
                Text = Path.GetFileNameWithoutExtension(ButtonsEvents.PathLastSaveLevel)+": ",
                VerticalAlignment = VerticalAlignment.Center,
            };
            line.Widgets.Add(_LevelName);

            var savedRegion = new TextureRegion(Texture2D.FromFile(MyraEnvironment.GraphicsDevice, pathS));
            var unsavedRegion = new TextureRegion(Texture2D.FromFile(MyraEnvironment.GraphicsDevice, pathU));

            _savedIcon = new Image
            {
                Renderable = savedRegion,
                Width = 12,
                Height = 12,
                VerticalAlignment = VerticalAlignment.Center
            };
            _unsavedIcon = new Image
            {
                Renderable = unsavedRegion,
                Width = 12,
                Height = 12,
                VerticalAlignment = VerticalAlignment.Center,
                Visible = false
            };
            var holder = new Panel();
            holder.Widgets.Add(_savedIcon);
            holder.Widgets.Add(_unsavedIcon);
            line.Widgets.Add(holder);

            return line;
        }
        public static void SetDirty(bool dirty)
        {
            if (_savedIcon == null || _unsavedIcon == null) return;
            _savedIcon.Visible = !dirty;
            _unsavedIcon.Visible = dirty;
        }
        public static void SetName(string path)
        {
            if(_LevelName == null) return;
            _LevelName.Text = Path.GetFileNameWithoutExtension(path) + ": ";
        }
    }
}
