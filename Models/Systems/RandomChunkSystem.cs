using System;
using System.Collections.Generic;
using HelloMonogame.Extensions;
using HelloMonogame.Models.Entities;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models.Systems;

public class RandomChunkSystem(HelloMonogame helloMonogame, SpriteBatch spriteBatch, AnimatedSprite validTiles)
    : Entity
{
    private Character _player = null!;

    private HelloMonogame _helloMonogame = helloMonogame;
    private SpriteBatch _spriteBatch = spriteBatch;
    private Dictionary<Vector2, Chunk> _chunks = new();
    private readonly AnimatedSprite _validTiles = validTiles;

    public override void Initialize(List<Entity> entities)
    {
        base.Initialize(entities);

        _player = entities.GetEntity<Character>();
    }

    public override void Load()
    {
        base.Load();
        
        _validTiles.Load();

        foreach (var chunk in _chunks)
        {
            chunk.Value.Load();
        }
    }

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);
        
        var chunkOffsets = ChunkUtilities.GenerateChunkOffsetsFromPlayer(_player);
        
        foreach (var chunkOffset in chunkOffsets)
        {
            if (!_chunks.ContainsKey(chunkOffset))
            {
                Generate(chunkOffset);
                Console.WriteLine($"Generating: {chunkOffset}");
            }
        }
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var chunk in _chunks.Values)
            chunk.Draw();
    }

    public void Generate(Vector2 chunkPosition)
    {
        if (_chunks.ContainsKey(chunkPosition)) return;
        
        var chunk = new Chunk(chunkPosition * ChunkUtilities.ChunkSize(), _validTiles);

        _chunks[chunkPosition] = chunk;
    }
}