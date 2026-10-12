using GhostEngine.DesktopGL.Editor.Settings;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal static class PaletteList
    {
        private static Button _selected;

        public static string SelectedItem { get; private set; }

        public static VerticalStackPanel CreateList()
        {
            _selected = null;
            SelectedItem = null;

            return new VerticalStackPanel
            {
                Spacing = 2,
                Padding = new Thickness(10, 4),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
        }

        public static Label CreateHeader(string text, bool first = false)
        {
            return new Label
            {
                Text = text.ToUpper(),
                TextColor = ButtonsEventsSettings.DimTextColor,
                Margin = new Thickness(0, first ? 0 : 10, 0, 4)
            };
        }

        public static Button CreateItem(string name, Color previewColor)
        {
            var row = new HorizontalStackPanel { Spacing = 8 };
            row.Widgets.Add(new Panel
            {
                Width = 16,
                Height = 16,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidBrush(previewColor)
            });
            row.Widgets.Add(new Label
            {
                Text = name,
                VerticalAlignment = VerticalAlignment.Center
            });

            var button = new Button
            {
                Content = row,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Padding = new Thickness(8, 5),
                BorderThickness = new Thickness(0)
            };
            SetSelected(button, false);

            button.Click += (s, e) =>
            {
                if (_selected != null)
                {
                    SetSelected(_selected, false);
                }

                _selected = button;
                SelectedItem = name;
                SetSelected(button, true);
            };

            return button;
        }

        private static void SetSelected(Button button, bool selected)
        {
            var normal = new SolidBrush(selected ? ButtonsEventsSettings.AccentColor : Color.Transparent);
            var hover = new SolidBrush(selected ? ButtonsEventsSettings.AccentColor : Color.Gray * 0.2f);

            button.Background = normal;
            button.FocusedBackground = normal; 
            button.OverBackground = hover;
            button.PressedBackground = hover;

            if (button.Content is HorizontalStackPanel row && row.Widgets.Count > 1 && row.Widgets[1] is Label label)
            {
                label.TextColor = selected ? Color.White : Stylesheet.Current.LabelStyle.TextColor;
            }
        }
    }
}
