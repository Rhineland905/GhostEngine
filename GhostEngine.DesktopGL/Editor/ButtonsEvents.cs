using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor
{
    internal class ButtonsEvents
    {
        public static void HandleMenuEvent(string Type)
        {
            switch (Type)
            {
                case "New":
                case "Open":
                case "Save":
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
                case "Exit":
                    Console.WriteLine("✓ Выход из приложения");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
