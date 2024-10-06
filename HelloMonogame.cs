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
    
    private readonly List<ILoadable> _loadables = [];
    private readonly List<IUpdateable> _updatables = [];
    private readonly List<IDrawable> _drawables = [];
    private readonly List<Entity> _entities = [];
    private Dictionary<MessageType, object> _messages = new();

    private Dictionary<Vector2, Chunk> _chunks = new ();

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

        var sprite = new SpriteMap(this, _spriteBatch, "Sprites/Selector.png", 16, 16);
        var animatedSprite = new AnimatedSprite(this, _spriteBatch,"Animations/Grass", new DefaultAnimatedSpriteOptions(sprite));
        
        _chunks.Add(new Vector2(0, 0),
            new Chunk(new Vector2(0, 0), new Dictionary<string, AnimatedSprite> { { "grass", animatedSprite } }));
        
        _entities.Add(new Character(this, _spriteBatch));
        _entities.Add(new InputSystem());
        
        _entities.Add(systemEntity);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        foreach (var entity in _entities)
            entity.Load();
        
        foreach (var chunk in _chunks.Values)
            chunk.Load();
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
        
        _entities.ForEach(entity => entity.Draw());
        
        var player = _entities.First(entity => entity is Character);

        var chunkOffsets = new List<Vector2>();
        
        for (var x = 0; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                chunkOffsets.Add(new Vector2(x, y));
            }
        }
        
        chunkOffsets = chunkOffsets.Select(offset => ChunkUtilities.WorldToChunkCoordinate(player.Position) + offset).ToList();
        
        foreach (var chunkOffset in chunkOffsets)
        {
            var chunk = _chunks.GetValueOrDefault(chunkOffset);
            
            chunk?.Draw();
        }
        
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
