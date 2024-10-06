using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Chunk : Entity
{
    private readonly Dictionary<Vector2, Tile> _tiles = new();

    private const int Seed = 1;
    private const int Size = 16;

    public Chunk(Vector2 position, Dictionary<string, AnimatedSprite> animatedSprites, float depth = 0)
    {
        Position = position;

        for (var idx = 0; idx < Size; idx++)
        {
            for (var idy = 0; idy < Size; idy++)
            {
                var animatedSprite = animatedSprites.Values.First();
                
                _tiles.Add(new Vector2(idx, idy), new Tile(
                    new Vector2(idx * animatedSprite.SpriteMap.TileWidth, idy * animatedSprite.SpriteMap.TileHeight), animatedSprite, depth));
            }
        }
    }
    
    public override void Load()
    {
        base.Load();
        
        foreach (var tile in _tiles)
            tile.Value.Load();
    }
    
    public override void Draw()
    {
        base.Draw();
        
        foreach (var tile in _tiles)
        {
            tile.Value.Draw(Position);
        }
    }
    
    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);
        
        foreach (var tile in _tiles)
            tile.Value.Update(gameTime, entities);
    }
}