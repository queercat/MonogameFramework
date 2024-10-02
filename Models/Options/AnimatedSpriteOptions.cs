using System.Collections.Generic;

namespace HelloMonogame.Models.Options;

public record AnimatedSpriteOptions(
    SpriteMap SpriteMap,
    float SecondsPerFrame,
    bool Playing,
    Dictionary<string, int[]> Animations);