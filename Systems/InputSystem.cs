using System;
using System.Collections.Generic;
using HelloMonogame.Enums;
using HelloMonogame.Models;
using HelloMonogame.Models.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HelloMonogame.Systems;

public class InputSystem : System
{
    public override void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        messages[MessageType.InputUp] = Keyboard.GetState().IsKeyDown(Keys.W);
        messages[MessageType.InputLeft] = Keyboard.GetState().IsKeyDown(Keys.A);
        messages[MessageType.InputDown] = Keyboard.GetState().IsKeyDown(Keys.S);
        messages[MessageType.InputRight] = Keyboard.GetState().IsKeyDown(Keys.D);
    }
}