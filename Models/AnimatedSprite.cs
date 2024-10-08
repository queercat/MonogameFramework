using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HelloMonogame.Enums;
using HelloMonogame.Models.Configs;
using HelloMonogame.Models.Contracts;
using HelloMonogame.Models.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YamlDotNet.Serialization;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class AnimatedSprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, float scale, float rotation, SpriteOptions spriteOptions, AnimatedSpriteOptions animatedSpriteOptions, int tilesWidth = 1, int tilesHeight = 1) : IUpdateable, ILoadable
{
    public readonly SpriteMap SpriteMap = animatedSpriteOptions.SpriteMap;
    
    private float _elapsed = 0f;
    private int _frame = 0;
    private bool _playing = animatedSpriteOptions.Playing;
    public readonly Dictionary<string, AnimationConfig> Animations = animatedSpriteOptions.Animations;
    private float _secondsPerFrame = animatedSpriteOptions.SecondsPerFrame;
    private string _animationName = "";
    private float _rotation = rotation;
    private float _scale = scale;
    public float Depth = spriteOptions.LayerDepth;
    
    private int tilesWidth = tilesWidth;
    private int tilesHeight = tilesHeight;
    private SpriteOptions _spriteOptions = spriteOptions;

    public AnimatedSprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, string animationConfigPath, DefaultAnimatedSpriteOptions animatedSpriteOptions) : this(helloMonogame, spriteBatch, 1, 0, new DefaultSpriteOptions(), animatedSpriteOptions)
    {
        RegisterAnimations(Path.Join("Content", animationConfigPath) + ".yaml");
    }
    
    public AnimatedSprite Clone() => new AnimatedSprite(helloMonogame, spriteBatch, _scale, _rotation, _spriteOptions, new AnimatedSpriteOptions(SpriteMap, _secondsPerFrame, _playing, Animations), tilesWidth, tilesHeight);

    public void Update(GameTime gameTime, List<Entity> entities)
    {
        if (!_playing) return;
        
        _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(_elapsed > _secondsPerFrame)) return;
        
        _elapsed = 0;
        
        _frame++;
        _frame %= Animations[_animationName].FrameCount;
    }

    private void RegisterAnimations(string path)
    {
        var deserializer = new DeserializerBuilder().Build();
        var text = File.ReadAllText(path);

        foreach (var file in text.Split("---"))
        {
            if (file == "") continue;
            
            var animationConfig = deserializer.Deserialize<AnimationConfig>(file);

            if (_animationName == "")
            {
                _animationName = animationConfig.Name;
                _secondsPerFrame = animationConfig.SecondsPerFrame ?? 1.0f;
            }
            Animations[animationConfig.Name] = animationConfig;
        }
        
    }
    
    public void Play(string animationName)
    {
        if (animationName == _animationName && _playing) return;
        
        _frame = 0;
        _animationName = animationName;
        _playing = true;
        _secondsPerFrame = Animations[_animationName].SecondsPerFrame ?? 1.0f;
    }

    public void Flip()
    {
        _spriteOptions = _spriteOptions with { SpriteEffects = SpriteEffects.FlipHorizontally };
    }
    
    public void Unflip()
    {
        _spriteOptions = _spriteOptions with { SpriteEffects = SpriteEffects.None };
    }
    
    public void Play()
    {
        _playing = true;
    }

    public void SwitchAnimation(string animationName)
    {
        _frame = 0;
        _animationName = animationName;
    }

    public void Stop()
    {
        _playing = false;
    }

    public void Draw(Vector2 position)
    {
        Animations.TryGetValue(_animationName, out var frames);
        
        if (frames is null) throw new NullReferenceException($"Animation {_animationName} doesn't exist!");

        var animationConfig = Animations[_animationName];
        var frame = animationConfig.InitialFrame + _frame;
        
        if (Math.Abs(_spriteOptions.LayerDepth - Depth) > 0)
        {
            _spriteOptions = _spriteOptions with { LayerDepth = Depth };
        }
        
        SpriteMap.DrawTile(frame, position, _rotation, _scale, _spriteOptions, tilesWidth, tilesHeight);
    }
    
    public void Load()
    {
        SpriteMap.Load();
    }

    public void DrawDebugInformation(Vector2 position, Vector2 debugPosition, string debugText = "")
    {
        var font = helloMonogame.Content.Load<SpriteFont>("Arial");
        var text = debugText;
        
        spriteBatch.DrawString(font, text, debugPosition - new Vector2(8, 8), Color.Black);
    }
}