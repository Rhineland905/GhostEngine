using Microsoft.Xna.Framework;

namespace GhostEngine.DesktopGL.Editor.Model.Objects
{
    internal class GameObject
    {
        public GameObjectDefinition Definition { get; }

        public Point Cell { get; set; }

        public GameObject(GameObjectDefinition definition, Point cell)
        {
            Definition = definition;
            Cell = cell;
        }
    }
}
