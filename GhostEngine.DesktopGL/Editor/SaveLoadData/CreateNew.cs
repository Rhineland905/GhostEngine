using GhostEngine.DesktopGL.Editor.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.SaveLoadData
{
    internal class CreateNew
    {
        static public void Create(string path) {
            try
            {
                if (Directory.Exists(path))
                {
                    MessageBox.ShowError("This folder already exists");
                    return;
                }
                ButtonsEvents.PathLastProject = path;

                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + "\\Content");
                Directory.CreateDirectory(path + "\\Levels");
            }
            catch (Exception ex) { MessageBox.ShowError(ex.Message); }
            
        }
    }
}

