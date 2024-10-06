using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using HelloMonogame.Models.Configs;
using HelloMonogame.Models.Contracts;
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
            if (e is not IMovable) return false;
            if (e == this) return false;
            
            var sourceX = e.Position.X + 8;
            var sourceY = e.Position.Y;

            var targetX = Position.X;
            var targetY = Position.Y;

            var size = 16;

            return sourceX >= targetX && sourceX <= targetX + size && sourceY >= targetY && sourceY <= targetY + size;
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