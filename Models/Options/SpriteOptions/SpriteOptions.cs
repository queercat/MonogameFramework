using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models.Options.SpriteOptions;

public record SpriteOptions(
    string Name,
    Vector2 Origin,
    SpriteEffects SpriteEffects,
    float Scale = 1,
    float LayerDepth = 0
);

public record DefaultSpriteOptions() : SpriteOptions("Default",
    Vector2.Zero,
    SpriteEffects.None);