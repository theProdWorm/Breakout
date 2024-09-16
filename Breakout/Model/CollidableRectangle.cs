using SFML.Graphics;

namespace Breakout.Model;

public abstract class CollidableRectangle : ICollidable
{
    private RectangleShape _rectangle;

    public CollidableRectangle(RectangleShape rectangle)
    {
        _rectangle = rectangle;
    }

    public bool Intersects(RectangleShape other)
    {
        return other.Position.X < _rectangle.Position.X + _rectangle.Size.X &&
               other.Position.X > _rectangle.Position.X - other.Size.X &&
               other.Position.Y < _rectangle.Position.Y + _rectangle.Size.Y &&
               other.Position.Y > _rectangle.Position.Y - other.Size.Y;
    }

    public abstract void HandleCollision();
}