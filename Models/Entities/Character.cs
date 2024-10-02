using System;
using System.Collections.Generic;
using HelloMonogame.Enums;
using HelloMonogame.Models.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models.Entities;

public class Character : Entity
{
    AnimatedSprite _animatedSprite;
    
    public Character(HelloMonogame helloMonogame, SpriteBatch spriteBatch)
    {
        var spriteMap = new SpriteMap(helloMonogame, spriteBatch, "SpriteSheets/Character.png", 32, 32);
        _animatedSprite = new AnimatedSprite(helloMonogame, spriteBatch, "Animations/Character", new DefaultAnimatedSpriteOptions(spriteMap));
        
        Add(_animatedSprite);
    }

    public override void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        base.Update(gameTime, messages);
        
        var velocity = new Vector2(0, 0);

        if ((bool)messages[MessageType.InputUp])
        {
            velocity.Y -= 1;
            _animatedSprite.Play("WalkUp");
        }

        if ((bool)messages[MessageType.InputDown])
        {
            velocity.Y += 1;
            _animatedSprite.Play("WalkDown");
        }

        if ((bool)messages[MessageType.InputLeft])
        {
            velocity.X -= 1;
            _animatedSprite.Flip();
            _animatedSprite.Play("WalkRight");
        }

        if ((bool)messages[MessageType.InputRight])
        {
            velocity.X += 1;
            _animatedSprite.Unflip();
            _animatedSprite.Play("WalkRight");
        }

        if (velocity != Vector2.Zero)
            velocity.Normalize();
        
        Position += velocity * 2;
    }
    
    public override void Draw()
    {
        base.Draw();
        
        _animatedSprite.Draw(Position);
    }
}