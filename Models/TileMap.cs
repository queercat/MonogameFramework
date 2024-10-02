using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class TileMap : IDrawable, ILoadable, IUpdateable
{
    Dictionary<Vector2, Tile> _tiles = new();
    Vector2 _position;
    
    public void Add(Vector2 position, Tile tile)
    {
        _tiles.Add(position, tile);
    }
    
    public void Draw()
    {
        foreach (var tile in _tiles.Values)
            tile.Draw(_position);
    }


    public void Load()
    {
        foreach (var tile in _tiles.Values)
            tile.Load();
    }
    
    public void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        foreach (var tile in _tiles.Values)
            tile.Update(gameTime, messages);
    }
}