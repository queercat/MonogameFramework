using HelloMonogame.Models;
using HelloMonogame.Models.Options;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Entities;

public class ConveyorBelt : Entity
{
    private readonly AnimatedSprite _animatedSprite;

    public ConveyorBelt(HelloMonogame helloMonogame, SpriteBatch spriteBatch, Vector2 position)
    {
        var spriteMap = new SpriteMap(helloMonogame, spriteBatch, "SpriteSheets/Conveyor.png", 16, 16);

        _animatedSprite = new AnimatedSprite(helloMonogame, spriteBatch, "Animations/Conveyor", new DefaultAnimatedSpriteOptions(spriteMap));
        _animatedSprite.Depth = 1.0f;
        
        Position = WorldUtilities.ScreenToGrid(position);
        
        _animatedSprite.Load();
    }

    public override void Draw()
    {
        base.Draw();
        
        _animatedSprite.Draw(Position);
        _animatedSprite.Play();
    }
}