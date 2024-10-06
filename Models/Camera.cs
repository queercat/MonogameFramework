using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloMonogame.Models;

public class Camera(float rotation, float zoom, Vector2 position, Viewport viewport) : Entity
{
    private Viewport Viewport { get; set; } = viewport;
    public Vector2 Position { get; set; } = position;

    private float Zoom { get; set; } = zoom;
    private float Rotation { get; set; } = rotation;
    
    public Matrix TransformMatrix =>
        Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
        Matrix.CreateRotationZ(Rotation) *
        Matrix.CreateScale(Zoom) *
        Matrix.CreateTranslation(new Vector3(Viewport.Width * 0.5f, Viewport.Height * 0.5f, 0));
    
    public void Rotate(float rotation)
    {
        Rotation += rotation;
    }
}