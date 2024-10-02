using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Models;

public class TileMap : IDrawable, ILoadable
{
    Dictionary<Vector2, Tile> _tiles = new();
    
    public void Add(Vector2 position, Tile tile)
    {
        _tiles.Add(position, tile);
    }
    
    public void Draw()
    {
        foreach (var tile in _tiles.Values)
            tile.Draw();
    }


    public void Load()
    {
        foreach (var tile in _tiles.Values)
            tile.Load();
    }
}