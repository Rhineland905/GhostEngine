using GhostEngine.DesktopGL.Editor.Config;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D;
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

        private static TextureRegion _radioOff, _radioOn, _closeImage;

        // Current palette, for widgets that need colors beyond the stylesheet (e.g. the accent Apply button)
        public static Color AccentColor { get; private set; }
        public static Color AccentHoverColor { get; private set; }
        public static Color DimTextColor { get; private set; }
        public static Color BorderColor { get; private set; }
        public static Color InputBackgroundColor { get; private set; }
        public static Color SurfaceColor { get; private set; }

        public static void ApplyWhiteTheme()
        {
            DarkMode = false;
        }

        public static void ApplyBlackTheme()
        {
            DarkMode = true;
        }

        public static void UpdateGlobalStylesheet(bool isDark)
        {
            var sheet = Myra.Graphics2D.UI.Styles.Stylesheet.Current;

            // Palette (dark is close to VS / VS Code, light to Windows 11)
            var bgColor        = isDark ? new Color(30, 30, 30)    : new Color(243, 243, 243); // editor area
            var surfaceColor   = isDark ? new Color(45, 45, 48)    : new Color(255, 255, 255); // menu bar, popups
            var windowColor    = isDark ? new Color(37, 37, 38)    : new Color(250, 250, 250);
            var borderColor    = isDark ? new Color(63, 63, 70)    : new Color(204, 204, 204);
            var buttonColor    = isDark ? new Color(62, 62, 66)    : new Color(229, 229, 229);
            var hoverColor     = isDark ? new Color(80, 80, 86)    : new Color(214, 214, 214);
            var accentColor    = isDark ? new Color(0, 122, 204)   : new Color(0, 120, 212);
            var selectionColor = isDark ? new Color(9, 71, 113)    : new Color(204, 228, 247);
            var textColor      = isDark ? new Color(230, 230, 230) : new Color(30, 30, 30);
            var dimTextColor   = isDark ? new Color(150, 150, 150) : new Color(110, 110, 110);
            var inputBackgroundColor = isDark ? new Color(30, 30, 30) : new Color(0,0,0);

            AccentColor = accentColor;
            AccentHoverColor = isDark ? new Color(28, 151, 234) : new Color(16, 110, 190);
            DimTextColor = dimTextColor;
            BorderColor = borderColor;
            InputBackgroundColor = inputBackgroundColor;
            SurfaceColor = surfaceColor;

            // Brush
            var bgBrush = new SolidBrush(bgColor);
            var surfaceBrush = new SolidBrush(surfaceColor);
            var windowBrush = new SolidBrush(windowColor);
            var borderBrush = new SolidBrush(borderColor);
            var buttonBrush = new SolidBrush(buttonColor);
            var hoverBrush = new SolidBrush(hoverColor);
            var accentBrush = new SolidBrush(accentColor);
            var selectionBrush = new SolidBrush(selectionColor);

            // Text
            sheet.LabelStyle.TextColor = textColor;
            sheet.LabelStyle.DisabledTextColor = dimTextColor;

            // HorizontalMenu (top bar)
            sheet.HorizontalMenuStyle.Background = surfaceBrush;
            sheet.HorizontalMenuStyle.Border = borderBrush;
            sheet.HorizontalMenuStyle.BorderThickness = new Thickness(0, 0, 0, 1);
            sheet.HorizontalMenuStyle.Padding = new Thickness(4, 2);
            sheet.HorizontalMenuStyle.LabelStyle.TextColor = textColor;
            sheet.HorizontalMenuStyle.SelectionHoverBackground = hoverBrush;
            sheet.HorizontalMenuStyle.SelectionBackground = selectionBrush;

            // VerticalMenu (dropdown)
            sheet.VerticalMenuStyle.Background = surfaceBrush;
            sheet.VerticalMenuStyle.Border = borderBrush;
            sheet.VerticalMenuStyle.BorderThickness = new Thickness(1);
            sheet.VerticalMenuStyle.Padding = new Thickness(2, 4);
            sheet.VerticalMenuStyle.LabelStyle.TextColor = textColor;
            sheet.VerticalMenuStyle.ShortcutStyle.TextColor = dimTextColor;
            sheet.VerticalMenuStyle.SelectionHoverBackground = selectionBrush;
            sheet.VerticalMenuStyle.SelectionBackground = selectionBrush;

            // Button
            sheet.ButtonStyle.Background = buttonBrush;
            sheet.ButtonStyle.OverBackground = hoverBrush;
            sheet.ButtonStyle.PressedBackground = accentBrush;
            sheet.ButtonStyle.Border = borderBrush;
            sheet.ButtonStyle.BorderThickness = new Thickness(1);

            // Separators
            var lineRegion = new ColoredRegion(DefaultAssets.WhiteRegion, borderColor);
            sheet.HorizontalSeparatorStyle.Image = lineRegion;
            sheet.VerticalSeparatorStyle.Image = lineRegion;
            sheet.HorizontalMenuStyle.SeparatorStyle.Image = lineRegion;
            sheet.VerticalMenuStyle.SeparatorStyle.Image = lineRegion;

            // RadioButton: default dots are white/grey and vanish on a light background,
            // so tint the original images (kept from the first call) per theme
            var radio = sheet.RadioButtonStyle.ImageStyle;
            _radioOff ??= radio.Image as TextureRegion;
            _radioOn ??= radio.PressedImage as TextureRegion;
            if (_radioOff != null && _radioOn != null)
            {
                radio.Image = new ColoredRegion(_radioOff, isDark ? Color.White : borderColor);
                radio.OverImage = new ColoredRegion(_radioOff, textColor);
                radio.PressedImage = new ColoredRegion(_radioOn, accentColor);
            }

            // Window
            sheet.WindowStyle.Background = windowBrush;
            sheet.WindowStyle.Border = accentBrush;
            sheet.WindowStyle.BorderThickness = new Thickness(1);
            sheet.WindowStyle.TitleStyle.TextColor = textColor;

            // Close "x" is white by default and invisible on the light window
            var close = sheet.WindowStyle.CloseButtonStyle.ImageStyle;
            _closeImage ??= close.Image as TextureRegion;
            if (_closeImage != null)
            {
                close.Image = new ColoredRegion(_closeImage, textColor);
                close.OverImage = new ColoredRegion(_closeImage, accentColor);
                close.PressedImage = new ColoredRegion(_closeImage, accentColor);
            }

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
        public static void ApplyButton()
        {
            ButtonsEventsSettings.UpdateGlobalStylesheet(ButtonsEventsSettings.DarkMode);
            ButtonsEventsSettings.RefreshUI();

            ButtonsEvents.SettingsWindowInstance = null;
            ButtonsEvents.HandleMenuEvent("Settings");
        }
    }
}