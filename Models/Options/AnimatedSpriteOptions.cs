namespace HelloMonogame.Models.Options;

public record AnimatedSpriteOptions(SpriteMap SpriteMap, float SecondsPerFrame, int[] FrameIds, bool Playing);