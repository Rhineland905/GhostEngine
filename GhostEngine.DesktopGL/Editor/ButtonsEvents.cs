using GhostEngine.DesktopGL.Editor.Settings;
using Myra.Graphics2D.UI;
using System;

namespace GhostEngine.DesktopGL.Editor
{
    internal class ButtonsEvents
    {
        public static Desktop DesktopInstance { get; set; }
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
                    Console.WriteLine("✓ Открыт файл");
                    break;
                case "Save":
                    Console.WriteLine("✓ Файл сохранен");
                    break;
                case "Settings":
                    var myWindow = SettingsCore.CreateSettingsWindow();
                    myWindow.Show(DesktopInstance);
                    break;
                case "Exit":

                    Environment.Exit(0);
                    break;
            }
        }
    }
}
