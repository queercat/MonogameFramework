using System;
using System.Collections.Generic;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HelloMonogame.Systems;

public class InputSystem : Entity
{
    public event EventHandler OnInputUp;
    public event EventHandler OnInputDown;
    public event EventHandler OnInputLeft;
    public event EventHandler OnInputRight;
    public event EventHandler<Vector2> OnMouseClick;
    
    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W))
            OnInputUp?.Invoke(this, EventArgs.Empty);
        
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            OnInputDown?.Invoke(this, EventArgs.Empty);
        
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            OnInputLeft?.Invoke(this, EventArgs.Empty);
        
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            OnInputRight?.Invoke(this, EventArgs.Empty);

        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var camera = entities.GetEntity<Camera>();
            var invertedMatrix = Matrix.Invert(camera.TransformMatrix);
            OnMouseClick?.Invoke(this, Vector2.Transform(Mouse.GetState().Position.ToVector2(), invertedMatrix));
        }
    }
}