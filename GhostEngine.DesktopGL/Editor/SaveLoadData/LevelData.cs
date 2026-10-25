using System.Collections.Generic;

namespace GhostEngine.DesktopGL.Editor.SaveLoadData
{
    // How a level looks in the .json file: only plain numbers, no references
    internal class LevelData
    {
        public int Width { get; set; }
        public int Height { get; set; }

        // Tile Id per cell, row by row from the top; 0 = empty. Cell (x, y) is at y * Width + x
        public int[] Tiles { get; set; }

        public List<ObjectData> Objects { get; set; } = new List<ObjectData>();
    }

    // One placed object in the file: type Id and its cell
    internal class ObjectData
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
