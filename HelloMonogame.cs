using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Enums;
using HelloMonogame.Models;
using HelloMonogame.Models.Contracts;
using HelloMonogame.Models.Entities;
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
        
        var systemEntity = new Entity();
        systemEntity.AddUpdatable(new InputSystem());
        systemEntity.AddUpdatable(new DebugSystem());
        
        _entities.Add(systemEntity);
        _entities.Add(new Character(this, _spriteBatch));
       
        base.Initialize();
    }

    protected override void LoadContent()
    {
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

        var player = _entities.First(entity => entity is Character);
        _camera.Position = Vector2.Lerp(_camera.Position, player.Position, 0.05f);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
       
        _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix, samplerState: SamplerState.PointClamp);
        
        _entities.ForEach(entity => entity.Draw());
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
