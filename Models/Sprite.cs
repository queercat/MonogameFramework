using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class Sprite(HelloMonogame helloMonogame, SpriteBatch spriteBatch, string path, Vector2 position, Vector2? velocity = null, Vector2? origin = null, SpriteEffects? spriteEffects = null, float rotation = 0f, float scale = 1f, float layerDepth = 0f) : ILoadable, IDrawable
{
    private readonly HelloMonogame _helloMonogame = helloMonogame;
    private readonly SpriteBatch _spriteBatch = spriteBatch;
    private readonly string _path = path;
    private Texture2D _texture;
    private Vector2 _position = position;
    private float _rotation = rotation;
    private Vector2 _velocity = velocity ?? new Vector2(0, 0);
    private Vector2 _origin = origin ?? new Vector2(0, 0);
    private SpriteEffects _spriteEffects = spriteEffects ?? SpriteEffects.None;
    private float _layerDepth = layerDepth;
    
    public void Load()
    {
        _texture = TextureLoader.LoadTexture(_helloMonogame.GraphicsDevice, _path);
    }

    public void Draw()
    {
        _spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, _origin, scale, _spriteEffects, _layerDepth);
    }
}