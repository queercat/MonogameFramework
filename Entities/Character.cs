using System;
using System.Collections.Generic;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using HelloMonogame.Models.Options;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Entities;

public class Character : Entity
{
    public Vector2 Velocity = new Vector2(0, 0);
    private readonly AnimatedSprite _animatedSprite;
    
    public float Speed = .1f;
    
    public Character(HelloMonogame helloMonogame, SpriteBatch spriteBatch)
    {
        var spriteMap = new SpriteMap(helloMonogame, spriteBatch, "SpriteSheets/Deer.png", 32, 32);
        _animatedSprite = new AnimatedSprite(helloMonogame, spriteBatch, "Animations/Character", new DefaultAnimatedSpriteOptions(spriteMap));

        Depth = 1.0f;
        
        Add(_animatedSprite);
    }

    public override void Initialize(List<Entity> entities)
    {
        base.Initialize(entities);

        var inputSystem = entities.GetEntity<InputSystem>();
        
        inputSystem.OnInputUp += (sender, e) => HandleMovement(sender, e, new Vector2(0, -1));
        inputSystem.OnInputDown += (sender, e) => HandleMovement(sender, e, new Vector2(0, 1));
        inputSystem.OnInputLeft += (sender, e) => HandleMovement(sender, e, new Vector2(-1, 0));
        inputSystem.OnInputRight += (sender, e) => HandleMovement(sender, e, new Vector2(1, 0));
    }

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);
        
        if (Velocity != Vector2.Zero) Velocity.Normalize();
        
        Position += Velocity * gameTime.ElapsedGameTime.Milliseconds * Speed;
        
        if (Math.Abs(Velocity.X) <= .2) { _animatedSprite.Play("Idle");}
        else if (Velocity.X > 0) { _animatedSprite.Play("WalkRight"); _animatedSprite.Unflip(); }
        else if (Velocity.X < 0) { _animatedSprite.Play("WalkRight"); _animatedSprite.Flip(); }
        
        Velocity = Vector2.Zero;
    }

    private void HandleMovement(object? sender, EventArgs e, Vector2 direction)
    {
        Velocity += direction;
    }
    
    public override void Draw()
    {
        base.Draw();
        
        _animatedSprite.Draw(Position);
    }
}