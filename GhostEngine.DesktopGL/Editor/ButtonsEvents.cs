using GhostEngine.DesktopGL.Editor.Config;
using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.SaveLoadData;
using GhostEngine.DesktopGL.Editor.Settings;
using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.File;
using System;
using System.IO;

namespace GhostEngine.DesktopGL.Editor
{
    internal class ButtonsEvents
    {
        public static Desktop DesktopInstance { get; set; }
        public static Window SettingsWindowInstance { get; set; }
        public static string PathLastProject { get; set; }
        public static string PathLastSaveLevel { get; set; }
        public static void HandleMenuEvent(string Type)
        {
            switch (Type)
            {   
                case "New Project":
                case "Open Project":
                case "New Level":
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
                
                case "New Project":
                    try
                    {
                        var dialogNewProject = new FileDialog(FileDialogMode.SaveFile) { Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) };
                        dialogNewProject.Closed += (s, a) =>
                        {
                            if (!dialogNewProject.Result) return;
                            PathLastProject = dialogNewProject.FilePath;
                            CreateNew.Create(dialogNewProject.FilePath);

                        };
                        dialogNewProject.ShowModal(DesktopInstance);
                    }
                    catch(Exception ex) { MessageBox.ShowError(ex.Message); }
                    break;
                case "Open Project":
                    try
                    {
                        var dialogOpenProject = new FileDialog(FileDialogMode.ChooseFolder) { Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)  };
                        dialogOpenProject.Closed += (s, a) =>
                        {
                            if (!dialogOpenProject.Result) return;
                            PathLastProject = dialogOpenProject.FilePath;
                        };
                        dialogOpenProject.ShowModal(DesktopInstance);
                    }
                    catch (Exception ex) { MessageBox.ShowError(ex.Message); }
                    break;
                case "Open":
                    try { 
                        var dialogOpen = new FileDialog(FileDialogMode.OpenFile) { Filter = "*.json", Folder = PathLastProject+"\\Levels", AutoAddFilterExtension = true };
                        dialogOpen.Closed += (s, a) =>
                        {
                            if (!dialogOpen.Result) return;
                            EditorState.CurrentLevel = SaveLevel.Load(dialogOpen.FilePath);
                        };
                        dialogOpen.ShowModal(DesktopInstance);
                    }
                    catch (Exception ex) { MessageBox.ShowError(ex.Message); }
                    break;
                case "Save":
                    try
                    {
                        if(PathLastSaveLevel == PathLastProject + "\\Levels\\") { 
                            if (PathLastSaveLevel == null) { PathLastSaveLevel = PathLastProject + "\\Levels\\"; }
                            var dialogSave = new FileDialog(FileDialogMode.SaveFile) { Filter = "*.json", FilePath = PathLastSaveLevel, AutoAddFilterExtension = true };
                            dialogSave.Closed += (s, a) =>
                            {
                                if (!dialogSave.Result) return;
                                PathLastSaveLevel = dialogSave.FilePath;
                                SaveLevel.Save(EditorState.CurrentLevel, dialogSave.FilePath);
                            };
                            dialogSave.ShowModal(DesktopInstance);
                        }else
                        {
                            SaveLevel.Save(EditorState.CurrentLevel, PathLastSaveLevel);
                        }
                    }
                    catch (Exception ex) { MessageBox.ShowError(ex.Message); }
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
                case "New Level":
                    try
                    {
                        EditorState.CurrentLevel = new Level(EditorState.DefaultLevelWidth, EditorState.DefaultLevelHeight);
                        var dialogNewLevel = new FileDialog(FileDialogMode.SaveFile) { Folder = PathLastProject + "\\Levels" };
                        dialogNewLevel.Closed += (s, a) =>
                        {
                            if (!dialogNewLevel.Result) return;
                            PathLastSaveLevel = dialogNewLevel.FilePath;
                        };
                        dialogNewLevel.ShowModal(DesktopInstance);
                    }
                    catch (Exception ex) { MessageBox.ShowError(ex.Message); }
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }
        }
        
    }
}
