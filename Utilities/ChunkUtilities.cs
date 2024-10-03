using System;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Systems;

public static class ChunkUtilities
{
    public static Vector2 WorldToChunkCoordinate(Vector2 position)
    {
        var chunkSize = 16 * 16;
        
        return new Vector2((int)Math.Floor(position.X / chunkSize), (int)Math.Floor(position.Y / chunkSize));
    }
}