using System;
using HelloMonogame.Models.Options;
using HelloMonogame.Models.Options.SpriteOptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class AnimatedSprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, Vector2 position, float scale, float rotation, SpriteOptions spriteOptions, AnimatedSpriteOptions animatedSpriteOptions) : IDrawable, IUpdatable, ILoadable
{
    private readonly SpriteMap _spriteMap = animatedSpriteOptions.SpriteMap;
    private float _elapsed = 0f;
    private int _frame = 0;
    private bool _playing = animatedSpriteOptions.Playing;
    private readonly int _frames = animatedSpriteOptions.Frames;
    private readonly float _secondsPerFrame = animatedSpriteOptions.SecondsPerFrame;
    private int _id = animatedSpriteOptions.Id;
    private Vector2 _position = position;
    private float _rotation = rotation;
    private float _scale = scale;

    public void Update(GameTime gameTime)
    {
        if (!_playing) return;
        
        _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(_elapsed > _secondsPerFrame)) return;
        
        _elapsed = 0;
        
        _frame++;
        _frame %= _frames;
    }
    
    public void Play()
    {
        _playing = true;
    }

    public new void Draw()
    {
        _spriteMap.DrawTile(_frame, _id, _position, _scale, _rotation, spriteOptions);
    }
    
    public new void Load()
    {
        _spriteMap.Load();
    }
}