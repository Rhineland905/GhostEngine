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
                Debug.WriteLine($"Error: {message}");
                return;
            }
            var box = Dialog.CreateMessageBox("Error", message);
            box.ButtonCancel.Visible = false;
            box.ShowModal(desktop);
        }
    }
}
