using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace GhostEngine.DesktopGL.Editor
{
    public class UIManager
    {
        public static Panel CreateMainUI()
        {
            var panel = new Panel();

            var line = new Panel
            {
                Height = 2,
                Background = new SolidBrush(Color.Gray)
            };

            panel.Widgets.Add(line);

            panel.Widgets.Add(line);
            var fileMenu = CreateFileButton();
            panel.Widgets.Add(fileMenu);

            return panel;
        }

        private static VerticalStackPanel CreateMenuButton(string title, dynamic[] items)
        {
            var btn = new Button
            {
                Content = new Label { Text = title },
                Width = 70,
                Height = 30
            };

            var submenu = new VerticalStackPanel { Visible = false };

            foreach (var item in items)
            {
                var subBtn = new Button
                {
                    Content = new Label { Text = $"{item.Name}   {item.Shortcut}" },
                    Width = 120,
                    Height = 25
                };
                subBtn.Click += (s, e) => ButtonsEvents.HandleMenuEvent(item.Name);
                submenu.Widgets.Add(subBtn);
            }

            btn.Click += (s, e) => submenu.Visible = !submenu.Visible;

            var container = new VerticalStackPanel();
            container.Widgets.Add(btn);
            container.Widgets.Add(submenu);

            return container;
        }




        private static HorizontalStackPanel CreateFileButton()
        {
            var menuBar = new HorizontalStackPanel();

            var fileBtn = CreateMenuButton("File", new[]
            {
                new { Name = "New", Shortcut = "Ctrl+N" },
                new { Name = "Open", Shortcut = "Ctrl+O" },
                new { Name = "Settings", Shortcut = "" },
                new { Name = "Exit", Shortcut = "Alt+F4" }
            });
            menuBar.Widgets.Add(fileBtn);

            return menuBar;


        }

        
    }
}
