using System.Collections.Generic;
using HelloMonogame.Entities;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Systems;

public class ChunkSystem(HelloMonogame helloMonogame, SpriteBatch spriteBatch, Dictionary<Vector2, Chunk> chunks) : Entity
{
    private HelloMonogame _helloMonogame = helloMonogame;
    private SpriteBatch _spriteBatch = spriteBatch;
    private Character _player;

    public override void Update(GameTime gameTime, List<Entity> entities)
    {
        base.Update(gameTime, entities);

        _player = entities.GetEntity<Character>();
    }

    public override void Draw()
    {
        base.Draw();
        
        foreach (var chunkOffset in ChunkUtilities.GenerateChunkOffsetsFromPlayer(_player))
        {
            chunks.TryGetValue(chunkOffset, out Chunk? value);
            value?.Draw();
        }
    }


}