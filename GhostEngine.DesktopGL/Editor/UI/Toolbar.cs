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
                Padding = new Thickness(0, 10), 
                Spacing = 4,
                Border = new SolidBrush( ButtonsEventsSettings.BorderColor),
                BorderThickness = new Thickness(0, 0, 1, 0),
                Background = new SolidBrush(ButtonsEventsSettings.SurfaceColor)
                
            };

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

            var searchHolder = new Panel
            {
                Background = null,
                Padding = new Thickness(10, 0),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            searchHolder.Widgets.Add(search);
            toolBar.Widgets.Add(searchHolder);

            var bGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderThickness = new Thickness(0, 0, 0, 1),
                Border = new SolidBrush( ButtonsEventsSettings.BorderColor),
            };
            bGrid.ColumnsProportions.Add(new Proportion(ProportionType.Part, 1));
            bGrid.ColumnsProportions.Add(new Proportion(ProportionType.Part, 1));

            var TabObjects = CreateTabObjects();
            var TabTiles = CreateTabTiles();

            var bTiles = new Button
            {
                Background = new SolidBrush(ButtonsEventsSettings.SurfaceColor),
                Content = new Label { Text = "Tiles", HorizontalAlignment = HorizontalAlignment.Center },
                Padding = new Thickness(8, 6),
                BorderThickness = new Thickness(0, 0, 0, 2),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                OverBackground = new SolidBrush(ButtonsEventsSettings.AccentHoverColor),
                PressedBackground = new SolidBrush(ButtonsEventsSettings.AccentHoverColor),


            };
            Grid.SetColumn(bTiles, 0);
            bGrid.Widgets.Add(bTiles);
            var bObject = new Button
            {
                Background = new SolidBrush(ButtonsEventsSettings.SurfaceColor),
                Content = new Label { Text = "Objects", HorizontalAlignment= HorizontalAlignment.Center },
                Padding = new Thickness(8, 6),
                BorderThickness = new Thickness(0, 0, 0, 2),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                OverBackground = new SolidBrush(ButtonsEventsSettings.AccentHoverColor),
                PressedBackground = new SolidBrush(ButtonsEventsSettings.AccentHoverColor),
            };

            bTiles.Click += (s, e) => ChoiceTab(true, bTiles, bObject, TabObjects, TabTiles);

            bObject.Click += (s,e) => ChoiceTab(false, bTiles, bObject, TabObjects, TabTiles);

            Grid.SetColumn(bObject, 1);
            bGrid.Widgets.Add(bObject);
            toolBar.Widgets.Add(bGrid);

            toolBar.Widgets.Add(TabObjects);
            toolBar.Widgets.Add(TabTiles);

            ChoiceTab(false, bTiles, bObject,TabObjects, TabTiles);
            

            

            return toolBar;
        }
        private static void ChoiceTab(bool titleObrject, Button bTiles, Button bObject, VerticalStackPanel TabObjects, VerticalStackPanel TabTiles)
        {
            if (!titleObrject)
            {
                bObject.Border = new SolidBrush(ButtonsEventsSettings.AccentColor);
                bTiles.Border = new SolidBrush(Color.Transparent);
                TabObjects.Visible = true;
                TabTiles.Visible = false;
            }
            else {
                bTiles.Border = new SolidBrush(ButtonsEventsSettings.AccentColor);
                bObject.Border = new SolidBrush(Color.Transparent);
                TabObjects.Visible = false;
                TabTiles.Visible = true;
            }
        }
        private static VerticalStackPanel CreateTabObjects()
        {
            var ObjectsTab = new VerticalStackPanel()
            {
            };
            var text = new Label
            {
                Text = "May",
                Width = 240
            };
            ObjectsTab.Widgets.Add(text);

            return ObjectsTab;
        }
        private static VerticalStackPanel CreateTabTiles()
        {
            var TilessTab = new VerticalStackPanel()
            {
            };
            var text = new Label
            {
                Text = "Gav",
                Width = 240
            };
            TilessTab.Widgets.Add(text);

            return TilessTab;
        }



    }
}
