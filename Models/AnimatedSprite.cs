using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Models.Contracts;
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
    private Dictionary<string, int[]> _animations = animatedSpriteOptions.Animations;
    private readonly float _secondsPerFrame = animatedSpriteOptions.SecondsPerFrame;
    private Vector2 _position = position;
    private string _animationName = "";
    private float _rotation = rotation;
    private float _scale = scale;
    
    private int tilesWidth = tilesWidth;
    private int tilesHeight = tilesHeight;

    public void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        if (!_playing) return;
        
        _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(_elapsed > _secondsPerFrame)) return;
        
        _elapsed = 0;
        
        _frame++;
        _frame %= _animations[_animationName].Length;
    }
    
    public void Translate(Vector2 translation)
    {
        _position += translation;
    }

    public void AddAnimation(string animationName, int[] frames)
    {
        _animations[animationName] = frames;
    }

    public void AddAnimation(string animationName, int initialFrame, int numberOfFrames)
    {
        var frames = new int[numberOfFrames];
        
        for (var idx = 0; idx < numberOfFrames; idx++)
        {
            frames[idx] = initialFrame + idx;
        }

        _animations[animationName] = frames;
    }
    
    public void Play(string animationName)
    {
        _animationName = animationName;
        _playing = true;
    }

    public void Flip()
    {
        spriteOptions = spriteOptions with { SpriteEffects = SpriteEffects.FlipHorizontally };
    }
    
    public void Unflip()
    {
        spriteOptions = spriteOptions with { SpriteEffects = SpriteEffects.None };
    }
    
    public void Play()
    {
        _playing = true;
    }

    public void Stop()
    {
        _playing = false;
    }

    public void Draw()
    {
        _animations.TryGetValue(_animationName, out var frames);
        
        if (frames is null) throw new NullReferenceException($"Animation {_animationName} doesn't exist!");
        
        _spriteMap.DrawTile(frames[_frame], _position, _rotation, _scale, spriteOptions, tilesWidth, tilesHeight);
    }
    
    public void Load()
    {
        _spriteMap.Load();
    }
}