using System;
using HelloMonogame.Models.Contracts;
using HelloMonogame.Models.Options;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = HelloMonogame.Models.Contracts.IDrawable;

namespace HelloMonogame.Models;

public class Sprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, SpriteOptions spriteOptions, string path, float rotation, Vector2 position)
    : ILoadable, IDrawable
{
    private Texture2D _texture = null!;
    private Vector2 _position = position;
    private float _rotation = rotation;
    
    private readonly Vector2 _origin = spriteOptions.Origin;
    private readonly SpriteEffects _spriteEffects = spriteOptions.SpriteEffects;
    private readonly float _layerDepth = spriteOptions.LayerDepth;
    private readonly string _path = path;
    private readonly float _scale = spriteOptions.Scale;

    public void Load()
    {
        _texture = TextureUtilities.LoadTexture(helloMonogame.GraphicsDevice, _path);
    }

    public void Draw()
    {
        if (_texture == null) throw new NullReferenceException("Texture is null");
        
        spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, _origin, _scale, _spriteEffects, _layerDepth);
    }
    
    public void Rotate(float rotation)
    {
        _rotation += rotation;
    }
    
    public void Translate(Vector2 translation)
    {
        _position += translation;
    }
}