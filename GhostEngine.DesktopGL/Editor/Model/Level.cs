using System.Collections.Generic;
using GhostEngine.DesktopGL.Editor.Model.Objects;
using Microsoft.Xna.Framework;

namespace GhostEngine.DesktopGL.Editor.Model
{
    internal class Level
    {
        public int Width { get; }
        public int Height { get; }

        private readonly TileDefinition[,] _tiles;

        // Placed objects; change them through PlaceObject / RemoveObjectAt
        private readonly List<GameObject> _objects = new List<GameObject>();
        public IReadOnlyList<GameObject> Objects => _objects;

        public Level(int width, int height)
        {
            Width = width;
            Height = height;
            _tiles = new TileDefinition[width, height];
        }

        public bool IsInside(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public TileDefinition GetTile(int x, int y)
        {
            return IsInside(x, y) ? _tiles[x, y] : null;
        }

        public void SetTile(int x, int y, TileDefinition tile)
        {
            if (IsInside(x, y))
            {
                _tiles[x, y] = tile;
            }
        }

        // Object standing in the cell, or null
        public GameObject GetObjectAt(int x, int y)
        {
            foreach (var gameObject in _objects)
            {
                if (gameObject.Cell.X == x && gameObject.Cell.Y == y)
                {
                    return gameObject;
                }
            }

            return null;
        }

        // One object per cell: replaces whatever stands there.
        // A unique type (player spawn) is moved instead of duplicated.
        public void PlaceObject(int x, int y, GameObjectDefinition definition)
        {
            if (!IsInside(x, y) || definition == null)
            {
                return;
            }

            RemoveObjectAt(x, y);

            if (definition.IsUnique)
            {
                _objects.RemoveAll(o => o.Definition == definition);
            }

            _objects.Add(new GameObject(definition, new Point(x, y)));
        }

        // true if an object was removed
        public bool RemoveObjectAt(int x, int y)
        {
            var gameObject = GetObjectAt(x, y);
            return gameObject != null && _objects.Remove(gameObject);
        }
    }
}
