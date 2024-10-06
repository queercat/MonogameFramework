using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace HelloMonogame.Utilities;

public static class WorldUtilities
{
    public static Vector2 ScreenToWorldCoordinates(Vector2 position)
    {
        return new Vector2((int)position.X / 16, (int)position.Y / 16);
    }

    public static Vector2 WorldToScreenCoordinates(Vector2 position) => position * 16;

    public static Vector2 ScreenToGrid(Vector2 position) =>
        WorldToScreenCoordinates(ScreenToWorldCoordinates(position));
}