using HelloMonogame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class SpriteMap(
    HelloMonogame helloMonogame,
    SpriteBatch spriteBatch,
    string name,
    string path,
    int width,
    int height)
    : ILoadable
{
    private string Path { get; } = path;
    public string Name { get; } = name;
    private HelloMonogame HelloMonogame { get; set; } = helloMonogame;
    private SpriteBatch SpriteBatch { get; set; } = spriteBatch;
    private Texture2D Texture { get; set; }
    private int Width { get; set; } = width;
    private int Height { get; set; } = height;

    private Rectangle GetTile(int x, int y)
    {
        return new Rectangle(x * Width, y * Height, Width, Height);
    }
    
    public void DrawTile(int x, int y, Vector2 position)
    {
        SpriteBatch.Draw(Texture, position, GetTile(x, y), Color.White);
    }

    private void LoadTexture()
    {
        Texture = TextureLoader.LoadTexture(HelloMonogame.GraphicsDevice, Path);
    }

    public void Load() {
        LoadTexture();
    }
}