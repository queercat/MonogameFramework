using System;
using HelloMonogame.Models.Options;
using HelloMonogame.Models.Options.SpriteOptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class AnimatedSprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, Vector2 position, float scale, float rotation, SpriteOptions spriteOptions, AnimatedSpriteOptions animatedSpriteOptions, int tilesWidth = 1, int tilesHeight = 1) : IDrawable, IUpdatable, ILoadable
{
    private readonly SpriteMap _spriteMap = animatedSpriteOptions.SpriteMap;
    private float _elapsed = 0f;
    private int _frame = 0;
    private bool _playing = animatedSpriteOptions.Playing;
    private readonly int[] _frames = animatedSpriteOptions.FrameIds;
    private readonly float _secondsPerFrame = animatedSpriteOptions.SecondsPerFrame;
    private Vector2 _position = position;
    private float _rotation = rotation;
    private float _scale = scale;
    
    private int tilesWidth = tilesWidth;
    private int tilesHeight = tilesHeight;

    public void Update(GameTime gameTime)
    {
        if (!_playing) return;
        
        _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(_elapsed > _secondsPerFrame)) return;
        
        _elapsed = 0;
        
        _frame++;
        _frame %= _frames.Length;
    }
    
    public void Translate(Vector2 translation)
    {
        _position += translation;
    }
    
    public void Play()
    {
        _playing = true;
    }

    public void Draw()
    {
        _spriteMap.DrawTile(_frames[_frame], _position, _rotation, _scale, spriteOptions, tilesWidth, tilesHeight);
    }
    
    public void Load()
    {
        _spriteMap.Load();
    }
}