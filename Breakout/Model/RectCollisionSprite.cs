using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class RectCollisionSprite(Texture texture) : Sprite(texture)
{
    public CollidableRectangle Collider { get; } = new((Vector2f)texture.Size);

    public new Vector2f Position
    {
        get => base.Position;
        set
        {
            base.Position = value;
            Collider.Position = value;
        }
    }

    public new Vector2f Scale
    {
        get => base.Scale;
        set
        {
            base.Scale = value;
            Collider.Scale = value;
        }
    }

    public Vector2f Size => new Vector2D(Collider.Size.X * Scale.X, Collider.Size.Y * Scale.Y);
}