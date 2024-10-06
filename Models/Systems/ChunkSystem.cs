using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Extensions;
using HelloMonogame.Models.Entities;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

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