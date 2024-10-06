using System.Collections.Generic;
using HelloMonogame.Extensions;
using HelloMonogame.Models.Entities;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models.Systems;

public class RandomChunkSystem(Character player, HelloMonogame helloMonogame, SpriteBatch spriteBatch, Dictionary<Vector2, Chunk> chunks)
    : Entity
{
    private Character _player = player;

    private HelloMonogame _helloMonogame = helloMonogame;
    private SpriteBatch _spriteBatch = spriteBatch;
    private Dictionary<Vector2, Chunk> _chunks = chunks;

    public override void Initialize(List<Entity> entities)
    {
        base.Initialize(entities);

        _player = entities.GetEntity<Character>();
    }
    
    public override void Draw()
    {
        base.Draw();

        var chunkOffsets = ChunkUtilities.GenerateChunkOffsetsFromPlayer(_player);

        foreach (var chunkOffset in chunkOffsets)
        {
            chunks.TryGetValue(chunkOffset, out var chunk);

            if (chunk is null)
            {
            }
        }
    }

    public void Generate(Vector2 chunkPosition)
    {
        
    }
}