using System.Collections.Generic;

namespace HelloMonogame.Models.Options;

public record AnimatedSpriteOptions(
    SpriteMap SpriteMap,
    float SecondsPerFrame,
    bool Playing,
    Dictionary<string, int[]> Animations
);

public record DefaultAnimatedSpriteOptions(SpriteMap SpriteMap) : AnimatedSpriteOptions(
    SpriteMap, 1.0f, true, new Dictionary<string, int[]>()
);