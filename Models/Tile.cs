using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Tile : Entity {
    private readonly AnimatedSprite _animatedSprite;
    
    public Tile(Vector2 position, AnimatedSprite animatedSprite, float depth = 0, string? animation = null)
    {
        Position = position;
        Depth = depth;
        
        _animatedSprite = animatedSprite;
        _animatedSprite.Depth = Depth;
        
        _animatedSprite.Stop();
        
        if (animation != null)
            _animatedSprite.SwitchAnimation(animation);
    }
    
    public void Draw(Vector2? offset, string debugInformation = "")
    {
        var position = Position + (offset ?? Vector2.Zero) * new Vector2(_animatedSprite.SpriteMap.TileWidth, _animatedSprite.SpriteMap.TileHeight);
        
        _animatedSprite.Draw(position);
        
        if (debugInformation.Length > 0)
            _animatedSprite.DrawDebugInformation(position, position, debugInformation);
    }
    
    public override void Load()
    {
        _animatedSprite.Load();
    }
}