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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    
    public override void Draw()
    {
        base.Draw();
        
        _animatedSprite.Draw(Position);
    }
}