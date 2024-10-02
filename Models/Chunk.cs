using System;
using System.Collections.Generic;
using HelloMonogame.Enums;
using Microsoft.Xna.Framework;
using IUpdateable = HelloMonogame.Models.Contracts.IUpdateable;

namespace HelloMonogame.Models;

public class Chunk : IDrawable, IUpdateable
{
    public Vector2 Position;
    public TileMap TileMap;
    public Dictionary<string, AnimatedSprite> AnimatedSprites = new ();
    
    private int _seed;
    private int _size = 16;
    
    public Chunk(Vector2 position, TileMap tileMap)
    {
        Position = position;
        TileMap = tileMap;

        _seed = (int)position.X * 1000 + (int)position.Y + 1000;

        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++)
            {
            }
        }
    }
    
    public void Draw()
    {
        TileMap.Draw();
    }
    
    public void Update(GameTime gameTime, Dictionary<MessageType, object> messages)
    {
        TileMap.Update(gameTime, messages);
    }
}