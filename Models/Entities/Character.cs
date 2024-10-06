using System;
using System.Collections.Generic;
using HelloMonogame.Enums;
using HelloMonogame.Extensions;
using HelloMonogame.Models.Options;
using HelloMonogame.Models.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models.Entities;

public class Character : Entity
{
    public Vector2 Velocity = new Vector2(0, 0);
    private readonly AnimatedSprite _animatedSprite;
    
    public Character(HelloMonogame helloMonogame, SpriteBatch spriteBatch)
    {
        var spriteMap = new SpriteMap(helloMonogame, spriteBatch, "SpriteSheets/Character.png", 32, 32);
        _animatedSprite = new AnimatedSprite(helloMonogame, spriteBatch, "Animations/Character", new DefaultAnimatedSpriteOptions(spriteMap));
        
        Add(_animatedSprite);
    }

    public override void Initialize(List<Entity> entities)
    {
        base.Initialize(entities);

        var inputSystem = entities.GetEntity<InputSystem>();
        
        inputSystem.OnInputUp += (sender, e) => HandleInput(sender, e, new Vector2(0, -1));
        inputSystem.OnInputDown += (sender, e) => HandleInput(sender, e, new Vector2(0, 1));
        inputSystem.OnInputLeft += (sender, e) => HandleInput(sender, e, new Vector2(-1, 0));
        inputSystem.OnInputRight += (sender, e) => HandleInput(sender, e, new Vector2(1, 0));
    }

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);
        
        Position += Velocity;
        Velocity = Vector2.Lerp(Velocity, Vector2.Zero, 0.1f);
    }

    private void HandleInput(object? sender, EventArgs e, Vector2 direction)
    {
        Velocity += direction;
        Velocity.Normalize();
    }
    
    public override void Draw()
    {
        base.Draw();
        
        _animatedSprite.Draw(Position);
    }
}