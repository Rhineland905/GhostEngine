using Microsoft.Xna.Framework;
﻿using GhostEngine.DesktopGL.Editor.Settings;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class Toolbar
    {
        public static VerticalStackPanel CreateToolbar()
        {
            var toolBar = new VerticalStackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch,
                Width = 240,
                Padding = new Thickness(10),
                Spacing = 4,
                Border = new SolidBrush( ButtonsEventsSettings.BorderColor),
                BorderThickness = new Thickness(0, 0, 1, 0),
                Background = new SolidBrush(ButtonsEventsSettings.SurfaceColor)
                
            };

            var separatorTools = new VerticalSeparator
            {
                Thickness = 2,
            };
            toolBar.Widgets.Add(separatorTools);

            var search = new TextBox
            {
                Border = new SolidBrush(ButtonsEventsSettings.BorderColor),
                BorderThickness = new Thickness(1, 1, 1, 1),
                Padding = new Thickness(8, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HintText = "Search",
                TextColor = ButtonsEventsSettings.DimTextColor,
                Background = new SolidBrush(ButtonsEventsSettings.InputBackgroundColor),
                ClipToBounds = true
            };
            toolBar.Widgets.Add(search);

            var layout = new Grid {
                ColumnSpacing = 20,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Border = new SolidBrush(ButtonsEventsSettings.BorderColor),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            layout.ColumnsProportions.Add(new Proportion(ProportionType.Auto)); // Tiles
            layout.ColumnsProportions.Add(new Proportion(ProportionType.Auto)); // Objects

            var tiles = CreateTab("Tiles");
            var objects = CreateTab("Objects");

            tiles.Click += (s, e) => { SetTabActive(tiles, true); SetTabActive(objects, false); };
            objects.Click += (s, e) => { SetTabActive(objects, true); SetTabActive(tiles, false); };

            SetTabActive(tiles, false);
            SetTabActive(objects, true);

            layout.Widgets.Add(tiles);
            layout.Widgets.Add(objects);

            Grid.SetColumn(tiles, 0);
            Grid.SetColumn(objects, 1);
            toolBar.Widgets.Add(layout);

            var separatorItems = new VerticalSeparator
            {
                Thickness = 2,
            };
            

            toolBar.Widgets.Add(separatorItems);
            return toolBar;
        }

        private static Button CreateTab(string text)
        {
            var transparent = new SolidBrush(Color.Transparent);

            return new Button
            {
                Width = 90,
                Padding = new Thickness(4, 6),
                Background = transparent,
                OverBackground = new SolidBrush(Color.White * 0.05f),
                PressedBackground = transparent,
                FocusedBackground = transparent,
                BorderThickness = new Thickness(0, 0, 0, 2),
                Content = new Label
                {
                    Text = text,
                    HorizontalAlignment = HorizontalAlignment.Center
                }
            };
        }

        private static void SetTabActive(Button tab, bool active)
        {
            tab.Border = new SolidBrush(active ? ButtonsEventsSettings.AccentColor : Color.Transparent);

            if (tab.Content is Label label)
            {
                label.TextColor = active
                    ? Myra.Graphics2D.UI.Styles.Stylesheet.Current.LabelStyle.TextColor
                    : ButtonsEventsSettings.DimTextColor;
            }
        }
    }
}
