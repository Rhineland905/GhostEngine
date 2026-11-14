using GhostEngine.DesktopGL.Editor.Settings;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class BotomLine
    {
        public static VerticalStackPanel Line()
        {
            var line = new VerticalStackPanel
            {
                Height = 20,
                Background = new SolidBrush(ButtonsEventsSettings.BorderColor)
            };
            return line;
        }
    }
}
