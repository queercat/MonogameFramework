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
        
        Camera = new Camera(0, 4, new Vector2(0, 0), GraphicsDevice.Viewport);

        _spriteMap = new SpriteMap(this, _spriteBatch, "Character", "SpriteSheets/Character.png", 32, 32);
        _character = new AnimatedSprite(this, _spriteBatch, Vector2.Zero, 1, 0, new DefaultSpriteOptions(),
            new AnimatedSpriteOptions(_spriteMap, .2f, false, new Dictionary<string, int[]>()), 1, 1);
       
        _character.AddAnimation("WalkRight", 16, 4);
        _character.AddAnimation("WalkDown", 12, 4);
        _character.AddAnimation("WalkUp", 20, 4);
        
        _character.Play("WalkUp");
       
        /* -- Loadables -- */
        _loadables.Add(_spriteMap);
        _loadables.Add(_character);
        
        /* -- Drawables -- */
        _drawables.Add(_character);
        
        /* -- Updatables -- */
        _updatables.Add(_character);
        
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
