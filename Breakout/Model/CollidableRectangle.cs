using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public abstract class CollidableRectangle : ICollidable
{
    private RectangleShape _rectangle;
    public RectangleShape Rectangle { get => _rectangle; }

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

    public bool ContainsPoint(Vector2f point)
    {
        return point.X > _rectangle.Position.X &&
               point.X < _rectangle.Position.X + _rectangle.Size.X &&
               point.Y > _rectangle.Position.Y &&
               point.Y < _rectangle.Position.Y + _rectangle.Size.Y;
    }

    public abstract void HandleCollision();
}