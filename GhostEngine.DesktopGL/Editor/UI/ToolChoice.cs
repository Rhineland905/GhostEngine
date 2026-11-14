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
using Microsoft.Xna.Framework;


namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class ToolChoice
    {
        private static readonly Dictionary<string, TextureRegion> _icons = new();
        private static readonly List<(Button button, EditorTool tool)> _buttons = new();

        public static VerticalStackPanel CreateToolPanel()
        {
            var toolChoice = new VerticalStackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch,
                Width = 60, 
                Padding = new Thickness(6, 10),
                Spacing = 10,
                Border = new SolidBrush(ButtonsEventsSettings.BorderColor),
                BorderThickness = new Thickness(0, 0, 1, 0),
                Background = new SolidBrush(ButtonsEventsSettings.SurfaceColor)

            };

            toolChoice.Widgets.Add(CreateToolButton("select", EditorTool.Select));
            toolChoice.Widgets.Add(CreateToolButton("paint", EditorTool.Paint));
            toolChoice.Widgets.Add(CreateToolButton("erase", EditorTool.Erase));
            toolChoice.Widgets.Add(CreateToolButton("move", EditorTool.Move));
            UpdateHighlight();
            return toolChoice;
        }

        private static TextureRegion LoadIcon(string name)
        {
            if (_icons.TryGetValue(name, out var cached)) return cached;
            var path = Path.Combine(AppContext.BaseDirectory, "Content", "Buttons", name + ".png");

            var region = new TextureRegion(Texture2D.FromFile(MyraEnvironment.GraphicsDevice, path));
            _icons[name] = region;
            return region;
        }
       
        private static Button CreateToolButton(string icon,EditorTool tool)
        {
            var button = new Button
            {
                Width = 48,
                Height = 48,
                BorderThickness = new Thickness(0),
                Content = new Image
                {
                    Renderable = LoadIcon(icon),
                    Width = 48,
                    Height = 48,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            button.Click += (s, e) => { EditorState.CurrentTool = tool; UpdateHighlight(); };
            _buttons.Add((button, tool));
            return button;
        }

        private static void UpdateHighlight()
        {
            foreach (var (button, tool) in _buttons)
            {
                bool active = tool == EditorState.CurrentTool;
                button.Background = new SolidBrush(active ? ButtonsEventsSettings.AccentColor : Color.Transparent);
                button.OverBackground = new SolidBrush(active ? ButtonsEventsSettings.AccentColor : Color.Gray * 0.2f);
            }
        }
        public static int CheckToolState()
        {
            switch (EditorState.CurrentTool)
            {
                case EditorTool.Select:
                    return 1;
                case EditorTool.Paint:
                    return 2;
                case EditorTool.Erase:
                    return 3;
                case EditorTool.Move:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
