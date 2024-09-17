using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class CollidableRectangle(Vector2f size) : RectangleShape(size)
{
    public bool Intersects(RectangleShape other)
    {
        return other.Position.X < Position.X + Size.X &&
               other.Position.X > Position.X - other.Size.X &&
               other.Position.Y < Position.Y + Size.Y &&
               other.Position.Y > Position.Y - other.Size.Y;
    }

    public bool ContainsPoint(Vector2f point)
    {
        return point.X > Position.X &&
               point.X < Position.X + Size.X &&
               point.Y > Position.Y &&
               point.Y < Position.Y + Size.Y;
    }
}