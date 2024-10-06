using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Extensions;
using HelloMonogame.Models.Entities;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;

namespace HelloMonogame.Models;

public class ChunkSystem(Dictionary<Vector2, Chunk> chunks) : Entity
{
    private Vector2 _playerChunkPosition;

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);

        var player = entities.GetEntity<Character>();
        _playerChunkPosition = ChunkUtilities.WorldToChunkCoordinate(player.Position);
    }

    public override void Draw()
    {
        base.Draw();
        
        var chunkOffsets = new List<Vector2>();
        
        for (var x = 0; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                chunkOffsets.Add(new Vector2(x, y));
            }
        }
        
        chunkOffsets = chunkOffsets.Select(offset => _playerChunkPosition + offset).ToList();

        foreach (var chunkOffset in chunkOffsets)
        {
            chunks.TryGetValue(chunkOffset, out Chunk? value);
            value?.Draw();
        }
    }
}