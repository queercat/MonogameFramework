using System;
using System.Collections.Generic;
using System.Linq;
using HelloMonogame.Enums;
using HelloMonogame.Extensions;
using HelloMonogame.Models;
using HelloMonogame.Models.Contracts;
using HelloMonogame.Models.Entities;
using HelloMonogame.Models.Options;
using HelloMonogame.Models.Systems;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = HelloMonogame.Models.IDrawable;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame;

public class HelloMonogame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera { get; set; }
    
    private readonly List<IUpdateable> _updatables = [];
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

        var spriteMap = new SpriteMap(this, _spriteBatch, "SpriteSheets/GrassSprites.png", 16, 16);
        
        _entities.Add(new Character(this, _spriteBatch));
        _entities.Add(new InputSystem());
        _entities.Add(new RandomChunkSystem(this, _spriteBatch, new AnimatedSprite(
            this, _spriteBatch, "Animations/Grass", new DefaultAnimatedSpriteOptions(spriteMap))));
        
        _entities.Add(systemEntity);
        
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
            updatable.Update(gameTime, _entities);
        
        foreach (var entity in _entities)
            entity.Update(gameTime, _entities);

        var player = _entities.GetEntity<Character>();
        _camera.Position = Vector2.Lerp(_camera.Position, player.Position, 0.05f);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
       
        _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix, samplerState: SamplerState.PointClamp);

        _entities.Sort((a, b) => a.Depth.CompareTo(b.Depth));
        _entities.ForEach(entity => entity.Draw());
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
