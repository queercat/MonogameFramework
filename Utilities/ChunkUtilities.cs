using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Models.Entities;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Systems;

public static class ChunkUtilities
{
    public static Vector2 WorldToChunkCoordinate(Vector2 position)
    {
        var chunkSize = 16 * 16;
        
        return new Vector2((int)Math.Floor(position.X / chunkSize), (int)Math.Floor(position.Y / chunkSize));
    }
    
    public static List<Vector2> GenerateChunkOffsetsFromPlayer(Character player)
    {
        var playerPosition = player.Position;
        var playerChunkPosition = WorldToChunkCoordinate(playerPosition);
        var chunkOffsets = new List<Vector2>();
        
        for (var x = 0; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                chunkOffsets.Add(new Vector2(x, y));
            }
        }
        
        chunkOffsets = chunkOffsets.Select(offset => playerPosition + offset).ToList();

        return chunkOffsets;
    }
}