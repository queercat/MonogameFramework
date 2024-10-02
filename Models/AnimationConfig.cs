namespace HelloMonogame.Models;

// ReSharper disable once ClassNeverInstantiated.Global
public class AnimationConfig
{
    public string Name { get; set; } = null!;
    public int InitialFrame { get; set; }
    public int FrameCount { get; set; }
    public float? FramesPerSecond { get; set; }
}