using GhostEngine.DesktopGL.Editor.Config;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static GhostEngine.DesktopGL.Editor.Config.SaveLoadConfig;

namespace GhostEngine.DesktopGL.Editor.Settings
{
 
    internal class ButtonsEventsSettings
    {
        public static Desktop DesktopInstance { get; set; }
        public static bool DarkMode { get; set; }

        public static void ApplyWhiteTheme()
        {
            DarkMode = false;
            UpdateGlobalStylesheet(DarkMode);
            
           
        }

        public static void ApplyBlackTheme()
        {
            DarkMode = true;
            UpdateGlobalStylesheet(DarkMode);
            
        }

        public static void UpdateGlobalStylesheet(bool isDark)
        {
            var sheet = Myra.Graphics2D.UI.Styles.Stylesheet.Current;

            // Color base
            var bgColor = isDark ? Color.Gray : Color.White;
            var topBarColor = isDark ? new Color(30, 30, 30) : new Color(240, 240, 240);
            var hoverColor = isDark ? new Color(62, 62, 66) : Color.LightGray;
            var selectionColor = isDark ? new Color(75, 75, 75) : Color.LightBlue;
            var textColor = isDark ? Color.White : Color.Black;
            var lineColor = isDark ? new Color(60, 60, 60) : Color.LightGray;
            var windowColor = isDark ? new Color(45, 45, 48) : Color.LightGray;

            // Brush
            var windBrush = new SolidBrush(windowColor);
            var bgBrush = new SolidBrush(bgColor);
            var topBarBrush = new SolidBrush(topBarColor);
            var hoverBrush = new SolidBrush(hoverColor);
            var selectionBrush = new SolidBrush(selectionColor);

            // HorizontalMenu
            sheet.HorizontalMenuStyle.Background = topBarBrush;
            sheet.HorizontalMenuStyle.LabelStyle.TextColor = textColor;
            sheet.HorizontalMenuStyle.SelectionBackground = selectionBrush;

            // VerticalMenu
            sheet.VerticalMenuStyle.Background = bgBrush;
            sheet.VerticalMenuStyle.LabelStyle.TextColor = textColor;
            sheet.VerticalMenuStyle.SelectionBackground = selectionBrush;

            // Button
            sheet.ButtonStyle.Background = topBarBrush;
            sheet.ButtonStyle.OverBackground = hoverBrush;
            sheet.ButtonStyle.PressedBackground = selectionBrush;
            sheet.LabelStyle.TextColor = textColor;

            var lineRegion = new ColoredRegion(DefaultAssets.WhiteRegion, lineColor);
            sheet.HorizontalSeparatorStyle.Image = lineRegion;
            sheet.VerticalSeparatorStyle.Image = lineRegion;

            //Winodw
            sheet.WindowStyle.Background = windBrush;
            sheet.WindowStyle.TitleStyle.TextColor = textColor;
            

            // Background
            sheet.PanelStyle.Background = bgBrush;
        }

        public static void RefreshUI()
        {
            if (DesktopInstance != null)
            {
                DesktopInstance.Root = UIManager.CreateMainUI() as Panel;
            }
        }
    }
}