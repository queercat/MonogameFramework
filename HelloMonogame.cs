using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HelloMonogame.Models;
using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloMonogame;

public class HelloMonogame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteMap _spriteMap;
    private Sprite _sprite;
    private AnimatedSprite _animatedSprite;
    private Camera Camera { get; set; }
    
    private readonly List<ILoadable> _loadables = [];
    private readonly List<IUpdatable> _updatables = [];

    public HelloMonogame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice); 
        _spriteMap = new SpriteMap(this, _spriteBatch, "Grass", "SpriteMaps/Grass.png", 16, 16);
        _sprite = new Sprite(this, _spriteBatch,  "egg", "Sprites/Egg.png", new Vector2(0, 0));
        _animatedSprite = new AnimatedSprite(this, _spriteBatch, "Grass", "SpriteMaps/Grass.png", 16, 16, 4, new Vector2(0, 0), playing: true);
        
        _loadables.Add(_spriteMap);
        _loadables.Add(_sprite);
        _loadables.Add(_animatedSprite);
        
        _updatables.Add(_animatedSprite);
        
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
        
        // draw texture
        _spriteBatch.Begin(transformMatrix: Camera.TransformMatrix, samplerState: SamplerState.PointClamp);
        // _spriteMap.DrawTile(1, 1, new Vector2(0, 0));
        // _sprite.Draw();
        _animatedSprite.Draw();
        _spriteBatch.End();
        

            
        base.Draw(gameTime);
    }
}
