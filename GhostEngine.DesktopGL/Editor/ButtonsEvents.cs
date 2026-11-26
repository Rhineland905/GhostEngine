using GhostEngine.DesktopGL.Editor.Config;
using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.SaveLoadData;
using GhostEngine.DesktopGL.Editor.Settings;
using GhostEngine.DesktopGL.Editor.UI;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.File;
using System;
using System.ComponentModel.DataAnnotations;
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
                    MessageBox.ShowError(Localization.Get("ButtonsEvents.Non"));
                    break;
            }
        }
        
        private static string LevelsFolder()
        {
            if (!string.IsNullOrEmpty(PathLastProject))
            {
                string levels = Path.Combine(PathLastProject, "Levels");
                if (Directory.Exists(levels)) return levels;
            }
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
                        var dialogOpen = new FileDialog(FileDialogMode.OpenFile) { Filter = "*.json", Folder = LevelsFolder(), AutoAddFilterExtension = true };
                        dialogOpen.Closed += (s, a) =>
                        {
                            if (!dialogOpen.Result) return;
                            var loaded = SaveLevel.Load(dialogOpen.FilePath);
                            if (loaded == null)
                            {
                                MessageBox.ShowError(Localization.Get("ButtonsEvents.CouldOpenTheLevel") + dialogOpen.FilePath);
                                return;
                            }
                            EditorState.CurrentLevel = loaded;
                            PathLastSaveLevel = dialogOpen.FilePath; // Ctrl+S now saves to the opened file
                            BotomLine.SetName(dialogOpen.FilePath);
                        };
                        dialogOpen.ShowModal(DesktopInstance);
                    }
                    catch (Exception ex) { MessageBox.ShowError(ex.Message); }
                    break;
                case "Save":
                    try
                    {
                        
                        if (string.IsNullOrEmpty(PathLastSaveLevel))
                        {
                            var dialogSave = new FileDialog(FileDialogMode.SaveFile) { Filter = "*.json", Folder = LevelsFolder(), AutoAddFilterExtension = true };
                            dialogSave.Closed += (s, a) =>
                            {
                                if (!dialogSave.Result) return;
                                if (SaveLevel.Save(EditorState.CurrentLevel, dialogSave.FilePath))
                                {
                                    EditorState.CurrentLevel.IsDirty = false;
                                    PathLastSaveLevel = dialogSave.FilePath;
                                }
                                else
                                {
                                    MessageBox.ShowError(Localization.Get("ButtonsEvents.CouldSaveTheLevelTo") + dialogSave.FilePath);
                                }
                            };
                            dialogSave.ShowModal(DesktopInstance);
                        }
                        else if (SaveLevel.Save(EditorState.CurrentLevel, PathLastSaveLevel))
                        {
                            EditorState.CurrentLevel.IsDirty = false;
                        }
                        else
                        {
                            MessageBox.ShowError(Localization.Get("ButtonsEvents.CouldSaveTheLevelTo") + PathLastSaveLevel);
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
                        // The current level is replaced only after both dialogs are confirmed
                        var dialogNewLevel = new FileDialog(FileDialogMode.SaveFile) { Filter = "*.json", Folder = LevelsFolder(), AutoAddFilterExtension = true };
                        dialogNewLevel.Closed += (s, a) =>
                        {
                            if (!dialogNewLevel.Result) return;
                            string path = dialogNewLevel.FilePath;
                            if (!string.Equals(Path.GetExtension(path), ".json", StringComparison.OrdinalIgnoreCase))
                            {
                                path += ".json";
                            }
                            var dialog = MessageBox.CreateLevel((width, height) =>
                            {
                                var level = new Level(width, height);
                                if (!SaveLevel.Save(level, path))
                                {
                                    MessageBox.ShowError(Localization.Get("ButtonsEvents.CouldCreateTheLevelFile") + path);
                                    return;
                                }
                                EditorState.CurrentLevel = level;
                                PathLastSaveLevel = path;
                            });
                            dialog.ShowModal(DesktopInstance);
                        };
                        dialogNewLevel.ShowModal(DesktopInstance);
                    }
                    catch (Exception ex) { MessageBox.ShowError(ex.Message); }
                    break;
                case "Exit":

                    MyraEnvironment.Game.Exit();
                    break;
            }
        }
        
    }
}
