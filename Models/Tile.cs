using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Tile : ILoadable, IUpdateable
{
    public Vector2 Position { get; set; }
    public int Depth { get; set; }
    
    private readonly AnimatedSprite _animatedSprite;
    
    public Tile(Vector2 position, AnimatedSprite animatedSprite, int depth = 0)
    {
        Position = position;
        Depth = depth;
        _animatedSprite = animatedSprite;
        _animatedSprite.Depth = depth;
    }
    
    public virtual void Draw(Vector2? offset)
    {
        var position = Position + (offset ?? Vector2.Zero) * new Vector2(_animatedSprite.SpriteMap.TileWidth, _animatedSprite.SpriteMap.TileHeight);
        
        _animatedSprite.Draw(position);
    }
    
    public virtual void Load()
    {
        _animatedSprite.Load();
    }

    public virtual void Update(GameTime gameTime)
    {
    }
}