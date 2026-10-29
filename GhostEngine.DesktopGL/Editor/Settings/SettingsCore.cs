using GhostEngine.DesktopGL.Editor.Config;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
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
                Width = 440,
                Height = 220 
            };

            var root = new VerticalStackPanel { Background = null };

            // Body
            var body = new VerticalStackPanel
            {
                Spacing = 10,
                Padding = new Thickness(20, 16, 20, 20),
                Background = null
            };

            body.Widgets.Add(new Label
            {
                Text = "APPEARANCE",
                TextColor = ButtonsEventsSettings.DimTextColor
            });

            var themeRow = new HorizontalStackPanel
            {
                Spacing = 24,
                Background = null
            };

            themeRow.Widgets.Add(new Label
            {
                Text = "Color theme",
                Width = 130,
                VerticalAlignment = VerticalAlignment.Center
            });

            bool darkMode = ButtonsEventsSettings.DarkMode;

            var whiteRadio = new RadioButton
            {
                Content = new Label { Text = "Light" },
                VerticalAlignment = VerticalAlignment.Center,
                CheckContentSpacing = 6,
                IsPressed = !darkMode
            };
            whiteRadio.Click += (s, e) => ButtonsEventsSettings.ApplyWhiteTheme();
            themeRow.Widgets.Add(whiteRadio);

            var blackRadio = new RadioButton
            {
                Content = new Label { Text = "Dark" },
                VerticalAlignment = VerticalAlignment.Center,
                CheckContentSpacing = 6,
                IsPressed = darkMode
            };
            blackRadio.Click += (s, e) => ButtonsEventsSettings.ApplyBlackTheme();
            themeRow.Widgets.Add(blackRadio);

            body.Widgets.Add(themeRow);

            body.Widgets.Add(new Label
            {
                Text = "Applied to the whole editor and saved.",
                TextColor = ButtonsEventsSettings.DimTextColor,
                Wrap = true
            });

            root.Widgets.Add(body);

            // Divider above the buttons
            root.Widgets.Add(new Panel
            {
                Height = 1,
                Background = new SolidBrush(ButtonsEventsSettings.BorderColor)
            });

            // Footer
            var footer = new HorizontalStackPanel
            {
                Spacing = 8,
                Padding = new Thickness(12),
                HorizontalAlignment = HorizontalAlignment.Right,
                Background = null
            };

            var cancelButton = new Button
            {
                Content = new Label { Text = "Cancel" },
                Padding = new Thickness(18, 6)
            };
            // Closed handler in ButtonsEvents drops the unapplied choice
            cancelButton.Click += (s, e) => window.Close();
            footer.Widgets.Add(cancelButton);

            var applyButton = new Button
            {
                Content = new Label { Text = "Apply", TextColor = Color.White },
                Padding = new Thickness(18, 6),
                Background = new SolidBrush(ButtonsEventsSettings.AccentColor),
                OverBackground = new SolidBrush(ButtonsEventsSettings.AccentHoverColor),
                PressedBackground = new SolidBrush(ButtonsEventsSettings.AccentHoverColor),
                Border = null
            };

            applyButton.Click += (s, e) =>
            {
                SaveData(new EngineData { IsDarkMode = ButtonsEventsSettings.DarkMode });
                window.Close();
                SettingsWindowInstance = null;
                ButtonsEventsSettings.ApplyButton();
            };
            footer.Widgets.Add(applyButton);

            root.Widgets.Add(footer);

            window.Content = root;
            SettingsWindowInstance = window;

            return window;
        }
    }
}
