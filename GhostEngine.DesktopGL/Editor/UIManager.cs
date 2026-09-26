using Myra;
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
            var fileMenu = CreateFileButton();  
            panel.Widgets.Add(fileMenu);
            return panel;
        }

       
        private static Panel CreateFileButton()
        {
            var button = new Button
            {
                Content = new Label { Text = "File" },
                Width = 70,
                Height = 30
            };
            button.Click += (s, e) => Console.WriteLine("File menu clicked");

            var container = new Panel
            {
                Width = 80,
                Height = 30
            };
            container.Widgets.Add(button);
            return container;
        }
    }
}
