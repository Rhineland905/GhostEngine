using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GhostEngine.DesktopGL.Editor.Settings
{
    internal class ButtonsEvents
    {
        public static Desktop DesktopInstance { get; set; }
        public static bool DarkMode { get; set; }

        public static void ApplyWhiteTheme()
        {
            DarkMode = false;
            ChoiceTheme(DesktopInstance?.Root, false);
            Console.WriteLine("✓ Белая тема применена");
        }

        public static void ApplyBlackTheme()
        {
            DarkMode = true;
            ChoiceTheme(DesktopInstance?.Root, true);
            Console.WriteLine("✓ Чёрная тема применена");
        }

        private static void ChoiceTheme(Widget widget, bool isDark)
        {
            if (widget == null) return;

            if (widget is Panel panel)
            {
                if (isDark)
                    panel.Background = new SolidBrush(Color.DarkGray);
                else
                    panel.Background = new SolidBrush(Color.White);
            }

            if (widget is IContainer container)
            {
                foreach (var child in container.Widgets)
                {
                    ChoiceTheme(child, isDark);
                }
            }
        }
    }
}
