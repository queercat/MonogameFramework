using Microsoft.Xna.Framework;

namespace HelloMonogame.Models;

public class Tile : IDrawable, ILoadable
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
    
    public virtual void Draw()
    {
        _animatedSprite.Draw(Position);
    }
    
    public virtual void Load()
    {
        _animatedSprite.Load();
    }
}