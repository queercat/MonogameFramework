using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Models.Contracts;

public interface IUpdatable
{
    void Update(GameTime gameTime, Dictionary<MessageType, object> messages);
}