using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.Settings;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.UI
{
    internal class MessageBox
    {
        public static void ShowError(string message)
        {
            var desktop = ButtonsEvents.DesktopInstance;
            if (desktop == null)
            {
                Debug.WriteLine($"{Localization.Get("MessageBox.Error")}: {message}");
                return;
            }
            var box = Dialog.CreateMessageBox(Localization.Get("MessageBox.Error"), message);
            box.ButtonCancel.Visible = false;
            box.ShowModal(desktop);
        }
        public static Window CreateLevel(Action<int, int> onCreate)
        {
            var grid = new Grid { ColumnSpacing = 8, RowSpacing = 8 };
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Fill));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            var window = new Dialog // Dialog = Window + OK/Cancel buttons
            {
                Title = Localization.Get("MessageBox.CreateNewLevel"),
                Width = 440,
                Height = 220
            };

            var widthLabel = new Label { Text = Localization.Get("MessageBox.Width") };
            Grid.SetRow(widthLabel, 0);
            Grid.SetColumn(widthLabel, 0);
            grid.Widgets.Add(widthLabel);

            var widthSpin = new SpinButton { Integer = true, Minimum = 8, Maximum = 512, Value = EditorState.DefaultLevelWidth };
            Grid.SetRow(widthSpin, 0);
            Grid.SetColumn(widthSpin, 1);
            grid.Widgets.Add(widthSpin);

            var heightLabel = new Label { Text = Localization.Get("MessageBox.Height") };
            Grid.SetRow(heightLabel, 1);
            Grid.SetColumn(heightLabel, 0);
            grid.Widgets.Add(heightLabel);

            var heightSpin = new SpinButton { Integer = true, Minimum = 8, Maximum = 512, Value = EditorState.DefaultLevelHeight };
            Grid.SetRow(heightSpin, 1);
            Grid.SetColumn(heightSpin, 1);
            grid.Widgets.Add(heightSpin);

            window.Content = grid;

            window.Closed += (s, a) =>
            {
                if (!window.Result) return; 

                int width = (int)(widthSpin.Value ?? EditorState.DefaultLevelWidth);
                int height = (int)(heightSpin.Value ?? EditorState.DefaultLevelHeight);
                onCreate(width, height);
            };

            return window;
        }
    }
}
