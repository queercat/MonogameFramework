using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Models.Entities;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Utilities;

public static class ChunkUtilities
{
    public static int ChunkSize() => 16;
    
    public static Vector2 WorldToChunkCoordinate(Vector2 position)
    {
        var chunkSize = ChunkSize() * 16;
        
        return new Vector2((int)Math.Floor(position.X / chunkSize), (int)Math.Floor(position.Y / chunkSize));
    }
    
    public static List<Vector2> GenerateChunkOffsetsFromPlayer(Character player)
    {
        var playerPosition = WorldToChunkCoordinate(player.Position);
        var chunkOffsets = new List<Vector2>();
        
        for (var x = -1; x <= 1; x++)
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