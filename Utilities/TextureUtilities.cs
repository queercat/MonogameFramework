using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Utilities;

public static class TextureUtilities
{
    public static Texture2D LoadTexture(GraphicsDevice graphicsDevice, string path)
    {
        return Texture2D.FromFile(graphicsDevice, Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Content", path));
    }
    
    public static Texture2D LoadTexture(GraphicsDevice graphicsDevice, params string[] paths)
    {
        return Texture2D.FromFile(graphicsDevice, Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Content", Path.Join(paths)));
    }
}