using GhostEngine.DesktopGL.Editor.Config;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Diagnostics;
using static GhostEngine.DesktopGL.Editor.Config.SaveLoadConfig;

namespace GhostEngine.DesktopGL.Editor.Settings
{
    internal class SettingsCore
    {
        public static Desktop DesktopInstance { get; set; }
        public static Window SettingsWindowInstance { get; set; }
        public static bool DarkMode { get; set; }


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

            var whiteRadio = new RadioButton
            {
                Content = new Label { Text = "Light" },
                VerticalAlignment = VerticalAlignment.Center
            };
            whiteRadio.Click += (s, e) => ButtonsEventsSettings.ApplyWhiteTheme();
            radioPanel.Widgets.Add(whiteRadio);

            var dividerLabel = new Label
            {
                Text = "|",
                TextColor = Color.DimGray
            };
            radioPanel.Widgets.Add(dividerLabel);

            var blackRadio = new RadioButton
            {
                Content = new Label { Text = "Dark" },
                VerticalAlignment = VerticalAlignment.Center
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
                window.Close();
                SettingsWindowInstance = null;
                ButtonsEventsSettings.RefreshUI();
                var newWindow = CreateSettingsWindow();

                if (newWindow != null && DesktopInstance != null && DesktopInstance.Root is Panel desktopRootPanel)
                {
                    desktopRootPanel.Widgets.Add(newWindow);
                }
                EngineData data = new EngineData
                {
                    IsDarkMode = ButtonsEventsSettings.DarkMode,
                };
                SaveLoadConfig.SaveData(data);
            };

            rootPanel.Widgets.Add(mainContent);
            rootPanel.Widgets.Add(applyButton);

            window.Content = rootPanel;
            SettingsWindowInstance = window;

            return window;
        }
        
    }
}