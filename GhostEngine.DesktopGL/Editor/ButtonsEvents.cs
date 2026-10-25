using GhostEngine.DesktopGL.Editor.Config;
using GhostEngine.DesktopGL.Editor.SaveLoadData;
using GhostEngine.DesktopGL.Editor.Settings;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.File;
using System;

namespace GhostEngine.DesktopGL.Editor
{
    internal class ButtonsEvents
    {
        public static Desktop DesktopInstance { get; set; }
        public static Window SettingsWindowInstance { get; set; }

        public static void HandleMenuEvent(string Type)
        {
            switch (Type)
            {
                case "New":
                case "Open":
                case "Save":
                case "Settings":
                case "Exit":
                    FileMenu(Type);
                    break;

                default:
                    Console.WriteLine($"Неизвестная команда: {Type}");
                    break;
            }
        }
        private static void FileMenu(string action)
        {
            
            switch (action)
            {
                
                case "New":
                    Console.WriteLine("✓ Создан новый файл");
                    break;
                case "Open":
                    var dialogOpen = new FileDialog(FileDialogMode.OpenFile) { Filter = "*.json", Folder = AppContext.BaseDirectory, AutoAddFilterExtension = true };
                    dialogOpen.Closed += (s, a) =>
                    {
                        EditorState.CurrentLevel = SaveLevel.Load(dialogOpen.FilePath);
                    };
                    dialogOpen.ShowModal(ButtonsEvents.DesktopInstance);
                    break;
                case "Save":
                    var dialogSave = new FileDialog(FileDialogMode.SaveFile) { Filter = "*.json", Folder = AppContext.BaseDirectory, AutoAddFilterExtension = true };
                    dialogSave.Closed += (s, a) =>
                    {
                        SaveLevel.Save(EditorState.CurrentLevel, dialogSave.FilePath);
                    };
                    dialogSave.ShowModal(ButtonsEvents.DesktopInstance);
                    break;
                case "Settings":

                    if (SettingsWindowInstance != null)
                    {
                        break;
                    }
                    var myWindow = SettingsCore.CreateSettingsWindow();
                    if (myWindow != null)
                    {
                        myWindow.Closed += (s, e) =>
                        {
                            ButtonsEventsSettings.DarkMode = SaveLoadConfig.LoadData().IsDarkMode;
                            SettingsWindowInstance = null;
                            if (DesktopInstance.Root is Panel rootPanel)
                            {
                                rootPanel.Widgets.Remove(myWindow);

                            }
                        };
                        if (DesktopInstance.Root is Panel rootPanel)
                        {
                            rootPanel.Widgets.Add(myWindow);
                        }

                        SettingsWindowInstance = myWindow;

                    }
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
