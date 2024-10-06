using Microsoft.Xna.Framework;

namespace HelloMonogame.Utilities;

public static class WorldUtilities
{
    public static Vector2 ScreenToWorldCoordinates(Vector2 position)
    {
        return new Vector2((int)position.X / 16, (int)position.Y / 16);
    }
}