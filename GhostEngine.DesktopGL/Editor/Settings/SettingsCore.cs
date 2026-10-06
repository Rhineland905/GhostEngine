using GhostEngine.DesktopGL.Editor.Config;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Diagnostics;
using System.Threading;
using static GhostEngine.DesktopGL.Editor.Config.SaveLoadConfig;

namespace GhostEngine.DesktopGL.Editor.Settings
{
    internal class SettingsCore
    {
        public static Window SettingsWindowInstance { get; set; }
    
        public static Window CreateSettingsWindow()
        {
            

            var window = new Window
            {
                Title = "Settings",
                Width = 500,
                Height = 400
            };

            var rootPanel = new Panel { Background = null };

            var mainContent = new VerticalStackPanel
            {
                Spacing = 20,
                Padding = new Thickness(20),
                Background = null
            };

            var separator1 = new Panel
            {
                Height = 1,
                Background = new SolidBrush(Color.DimGray * 0.5f)
            };
            mainContent.Widgets.Add(separator1);

            var themeRow = new VerticalStackPanel
            {
                Spacing = 5,
                Background = null
            };

            var radioPanel = new HorizontalStackPanel
            {
                Spacing = 15,
                Background = null
            };

            var themeLabel = new Label
            {
                Text = "Color Theme:",
                VerticalAlignment = VerticalAlignment.Center
            };
            radioPanel.Widgets.Add(themeLabel);
            bool DarkMode = ButtonsEventsSettings.DarkMode;
            bool WhitePressed = false, DarkPressed = false;

            if (!DarkMode)
            {
                WhitePressed = true;
            } 
            var whiteRadio = new RadioButton
            {
                Content = new Label { Text = "Light" },
                VerticalAlignment = VerticalAlignment.Center,
                IsPressed = WhitePressed
            };
            whiteRadio.Click += (s, e) => ButtonsEventsSettings.ApplyWhiteTheme();
            radioPanel.Widgets.Add(whiteRadio);

            var dividerLabel = new Label
            {
                Text = "|",
                TextColor = Color.DimGray
            };
            radioPanel.Widgets.Add(dividerLabel);

            if (DarkMode)
            {
                DarkPressed = true;
            }
            var blackRadio = new RadioButton
            {
                Content = new Label { Text = "Dark" },
                VerticalAlignment = VerticalAlignment.Center,
                IsPressed = DarkPressed
            };
            blackRadio.Click += (s, e) => ButtonsEventsSettings.ApplyBlackTheme();
            radioPanel.Widgets.Add(blackRadio);

            themeRow.Widgets.Add(radioPanel);
            mainContent.Widgets.Add(themeRow);

            var applyButton = new Button
            {
                Content = new Label { Text = "Apply" },
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 15, 15),
                Padding = new Thickness(15, 8),
                
                

            };

            applyButton.Click += (s, e) =>
            {
                SaveLoadConfig.SaveData(new EngineData { IsDarkMode = ButtonsEventsSettings.DarkMode });
                window.Close();
                SettingsWindowInstance = null;
                ButtonsEventsSettings.ApplyButton();

            };

            rootPanel.Widgets.Add(mainContent);
            rootPanel.Widgets.Add(applyButton);

            window.Content = rootPanel;
            SettingsWindowInstance = window;

            return window;
        }
        
    }
}