using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class AnimatedSprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, string name, string path, int tileWidth, int tileHeight, int frames, Vector2 position, Vector2? velocity = null, Vector2? origin = null, SpriteEffects? spriteEffects = null, float rotation = 0, float scale = 1, float layerDepth = 0, bool playing = false, float secondsPerFrame = 1f) : Sprite(helloMonogame, spriteBatch, name, path, position, velocity, origin, spriteEffects, rotation, scale, layerDepth), ILoadable, IDrawable, IUpdatable
{
    private readonly SpriteMap _spriteMap = new SpriteMap(helloMonogame, spriteBatch, name, path, tileWidth, tileHeight);
    private float _elapsed = 0f;
    private int _frame = 0;
    private bool _playing = playing;

    public void Update(GameTime gameTime)
    {
        if (!_playing) return;
        
        _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(_elapsed > secondsPerFrame)) return;
        
        _elapsed = 0;
        
        _frame++;
        _frame %= frames;
    }
    
    public void Play()
    {
        _playing = true;
    }

    public new void Draw()
    {
        _spriteMap.DrawTile(_frame, 0, position);
    }
    
    public new void Load()
    {
        _spriteMap.Load();
    }
}