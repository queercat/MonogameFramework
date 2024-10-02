using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Models.Contracts;

public interface IUpdatable
{
    void Update(GameTime gameTime, Dictionary<MessageType, object> messages);
}