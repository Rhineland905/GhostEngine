using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Diagnostics;

namespace GhostEngine.DesktopGL.Editor.Settings
{
    internal class SettingsCore
    {
        public static Window CreateSettingsWindow()
        {
            var window = new Window
            {
                Title = "Settings",
                Width = 500,
                Height = 400
            };

            var mainContent = new VerticalStackPanel
            {
                Spacing = 20,
                Padding = new Thickness(0)
            };

            var themeSectionLabel = new Label
            {
                Text = "GENERAL",
             
                TextColor = Color.LightGray
            };
            mainContent.Widgets.Add(themeSectionLabel);

            var separator1 = new Panel
            {
                Height = 1,
                Background = new SolidBrush(Color.DarkGray)
            };
            mainContent.Widgets.Add(separator1);

            var themeRow = new VerticalStackPanel
            {
                Spacing = 0
            };
            var radioPanel = new HorizontalStackPanel
            {
                Spacing = 10
            };
            var themeLabel = new Label
            {
                Text = "Color Theme:",
                Height = 25
            };
            radioPanel.Widgets.Add(themeLabel);

            

            var whiteRadio = new RadioButton
            {
                Content = new Label { Text = "Light" },
                Width = 50,
                
            };
            whiteRadio.Click += (s, e) => ButtonsEventsSettings.ApplyWhiteTheme();
            radioPanel.Widgets.Add(whiteRadio);
            var label = new Label { Text = "|",Left =15};
            radioPanel.Widgets.Add(label);
            var blackRadio = new RadioButton
            {
                Content = new Label { Text = "Dark" },
                Width = 50,
              
                Left = 20,
            };
            blackRadio.Click += (s, e) => ButtonsEventsSettings.ApplyBlackTheme();
            radioPanel.Widgets.Add(blackRadio);

            themeRow.Widgets.Add(radioPanel);
            mainContent.Widgets.Add(themeRow);

            

            window.Content = mainContent;
            return window;
        }
    }
}