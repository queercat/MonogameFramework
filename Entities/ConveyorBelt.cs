using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Models;
using HelloMonogame.Models.Configs;
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

        var animatedSpriteOptions =
            new DefaultAnimatedSpriteOptions(spriteMap);
        
        _animatedSprite = new AnimatedSprite(helloMonogame, spriteBatch, "Animations/Conveyor", animatedSpriteOptions);
        _animatedSprite.Depth = 1.0f;
        
        Position = WorldUtilities.ScreenToGrid(position);
        
        _animatedSprite.Load();
        _animatedSprite.Play();
    }

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);
        
        _animatedSprite.Update(gameTime, entities);

        var entitiesOnMe = entities.Where(e =>
        {
            if (e == this) return false;
            
            var sourceX = e.Position.X;
            var sourceY = e.Position.Y;

            var targetX = Position.X;
            var targetY = Position.Y;

            var size = 16;

            if (sourceX >= targetX && sourceX <= targetX + size)
            {
                if (sourceY >= targetY && sourceY <= targetY + size)
                {
                    return true;
                }
            }

            return false;
        }).ToList();
        
        foreach (var entity in entitiesOnMe)
        {
            entity.Position -= Vector2.UnitY;
        }
    }

    public override void Draw()
    {
        base.Draw();
        
        _animatedSprite.Draw(Position);
    }
}