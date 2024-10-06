using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models.Systems;

public class InputSystem : Entity
{
    public event EventHandler OnInputUp;
    public event EventHandler OnInputDown;
    public event EventHandler OnInputLeft;
    public event EventHandler OnInputRight;
    
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
    }
}