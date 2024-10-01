using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HelloMonogame.Models;
using HelloMonogame.Models.Options;
using HelloMonogame.Models.Options.SpriteOptions;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = HelloMonogame.Models.IDrawable;

namespace HelloMonogame;

public class HelloMonogame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteMap _spriteMap;
    private Sprite _sprite;
    private AnimatedSprite _animatedSprite;
    private AnimatedSprite _character;
    private Camera Camera { get; set; }
    
    private readonly List<ILoadable> _loadables = [];
    private readonly List<IUpdatable> _updatables = [];
    private readonly List<IDrawable> _drawables = [];

    public HelloMonogame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteMap = new SpriteMap(this, _spriteBatch, "Automation", "SpriteMaps/Automation.png", 16, 16);
        _animatedSprite = new AnimatedSprite(this, _spriteBatch, Vector2.Zero, 1, 0, new DefaultSpriteOptions(),
            new AnimatedSpriteOptions(_spriteMap, .1f, [200, 201, 202, 203, 204, 205, 206, 207], true), 1, 2);
        var characterSpriteMap = new SpriteMap(this, _spriteBatch, "Character", "SpriteMaps/Character.png", 16, 16);
        _character = new AnimatedSprite(this, _spriteBatch, Vector2.One, 1, 0, new DefaultSpriteOptions(),
            new AnimatedSpriteOptions(characterSpriteMap, .3f, [64, 66, 68, 70], true), 2, 4);
      
        _loadables.Add(_spriteMap);
        _loadables.Add(_animatedSprite);
        _loadables.Add(_character);
        
        _updatables.Add(_animatedSprite);
        _updatables.Add(_character);
        
        _drawables.Add(_animatedSprite);
        _drawables.Add(_character);
        
        Camera = new Camera(0, 4, new Vector2(0, 0), GraphicsDevice.Viewport);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        foreach (var loadable in _loadables)
        {
            loadable.Load();
        }
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var updatable in _updatables)
        {
            updatable.Update(gameTime);
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
       
        _spriteBatch.Begin(transformMatrix: Camera.TransformMatrix, samplerState: SamplerState.PointClamp);
        
        _drawables.ForEach(drawable => drawable.Draw());
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
