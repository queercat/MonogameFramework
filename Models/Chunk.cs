using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Chunk : IDrawable, IUpdateable, ILoadable
{
    public Vector2 Position;
    public Dictionary<string, AnimatedSprite> AnimatedSprites;
    private readonly Dictionary<Vector2, Tile> _tiles = new();

    private const int Seed = 1;
    private const int Size = 16;

    public Chunk(Vector2 position, Dictionary<string, AnimatedSprite> animatedSprites)
    {
        Position = position;
        AnimatedSprites = animatedSprites;

        for (var idx = 0; idx < Size; idx++)
        {
            for (var idy = 0; idy < Size; idy++)
            {
                var animatedSprite = AnimatedSprites.Values.First();
                
                _tiles.Add(new Vector2(idx, idy), new Tile(
                    new Vector2(idx * animatedSprite.SpriteMap.TileWidth, idy * animatedSprite.SpriteMap.TileHeight), animatedSprite));
            }
        }
        
    }
    
    public virtual void Load()
    {
        foreach (var tile in _tiles)
            tile.Value.Load();
    }
    
    public void Draw()
    {
        foreach (var tile in _tiles)
        {
            tile.Value.Draw(Position);
        }
    }
    
    public void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        foreach (var tile in _tiles)
            tile.Value.Update(gameTime, messages);
    }
}