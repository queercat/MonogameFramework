using System;
using HelloMonogame.Models.Options;
using HelloMonogame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class SpriteMap(
    HelloMonogame helloMonoGame,
    SpriteBatch spriteBatch,
    string path,
    int tileWidth,
    int tileHeight)
    : ILoadable
{
    private string _path { get; } = path;
    private HelloMonogame _helloMonoGame { get; set; } = helloMonoGame;
    private SpriteBatch _spriteBatch { get; set; } = spriteBatch;
    private Texture2D _texture { get; set; } = null!;
    public int TileWidth { get; set; } = tileWidth;
    public int TileHeight { get; set; } = tileHeight;

    private Rectangle GetTile(int x, int y)
    {
        return new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
    }

    private void DrawTile(int x, int y, Vector2 position, float rotation, float scale, SpriteOptions spriteOptions, int tilesWidth = 1, int tilesHeight = 1)
    {
        var sourceRectangle = GetTile(x, y);
     
        sourceRectangle.Width *= tilesWidth;
        sourceRectangle.Height *= tilesHeight;

        // TODO: Make this a mapping of percentages from spriteOptions.
        var origin = sourceRectangle.Size.ToVector2() / 2;
        
        _spriteBatch.Draw(_texture, position, sourceRectangle, Color.White, rotation, origin, scale, spriteOptions.SpriteEffects, 0);
    }

    public void DrawTile(int id, Vector2 position, float rotation, float scale, SpriteOptions spriteOptions, int tilesWidth = 1, int tilesHeight = 1)
    {
        // ReSharper disable once PossibleLossOfFraction
        double rows = _texture.Height / TileHeight;
        // ReSharper disable once PossibleLossOfFraction
        double columns = _texture.Width / TileWidth;
        
        if (Math.Abs(rows - (int)rows) > 0) throw new Exception("Size of texture is not divisible by tile height");
        if (Math.Abs(columns - (int)columns) > 0) throw new Exception("Size of texture is not divisible by tile width");

        var x = id % (int)columns;
        var y = id / (int)columns;
        
        DrawTile(x, y, position, rotation, scale, spriteOptions, tilesWidth, tilesHeight);
    }

    private void LoadTexture()
    {
        _texture = TextureUtilities.LoadTexture(_helloMonoGame.GraphicsDevice, _path);
    }

    public void Load() {
        LoadTexture();
    }
}