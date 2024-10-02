using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HelloMonogame.Models;
using HelloMonogame.Models.Contracts;
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
    private Camera _camera { get; set; }
    
    private readonly List<ILoadable> _loadables = [];
    private readonly List<IUpdatable> _updatables = [];
    private readonly List<IDrawable> _drawables = [];
    private readonly List<Entity> _entities = [];
    private Dictionary<MessageType, object> _messages = new();

    public HelloMonogame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _camera = new Camera(0, 4, new Vector2(0, 0), GraphicsDevice.Viewport);

        _spriteMap = new SpriteMap(this, _spriteBatch, "Character", "SpriteSheets/Character.png", 32, 32);
        _character = new AnimatedSprite(this, _spriteBatch, "Animations/Character",
            new DefaultAnimatedSpriteOptions(_spriteMap));
       
        _character.Play("WalkUp");

        var systemEntity = new Entity();
        systemEntity.AddUpdatable(new InputSystem());
        
        _entities.Add(systemEntity);
       
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
            loadable.Load();
        
        foreach (var entity in _entities)
            entity.Load();
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var updatable in _updatables)
            updatable.Update(gameTime, _messages);
        
        foreach (var entity in _entities)
            entity.Update(gameTime, _messages);

        if ((bool)_messages[MessageType.InputRight])
        {
            _character.Unflip();
            _character.Play("WalkRight");
        }
        
        if ((bool)_messages[MessageType.InputLeft])
        {
            _character.Flip();
            _character.Play("WalkRight");
        }

        if ((bool)_messages[MessageType.InputUp])
            _character.Play("WalkUp");
        
        if ((bool)_messages[MessageType.InputDown])
            _character.Play("WalkDown");


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
       
        _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix, samplerState: SamplerState.PointClamp);
        
        _drawables.ForEach(drawable => drawable.Draw());
        _entities.ForEach(entity => entity.Draw());
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
