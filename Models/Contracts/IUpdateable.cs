using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Models.Contracts;

public interface IUpdateable
{
    void Update(GameTime gameTime);
}