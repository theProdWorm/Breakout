using SFML.System;

namespace Breakout;

public class Vector2D(float x, float y)
{
    public static Vector2D One => new Vector2D(1, 1);
    public static Vector2D Zero => new Vector2D(0, 0);
    
    public static Vector2D Left => new Vector2D(-1, 0);
    public static Vector2D Right => new Vector2D(1, 0);
    public static Vector2D Up => new Vector2D(0, -1);
    public static Vector2D Down => new Vector2D(0, 1);
    
    public float X { get; } = x;
    public float Y { get; } = y;

    public float Magnitude => MathF.Sqrt(X * X + Y * Y);

    public Vector2D(float xy) : this(xy, xy)
    {
    }

    public static Vector2D operator +(Vector2D v1, Vector2D v2) => new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2D operator +(Vector2D v1, Vector2f v2) => new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2D operator +(Vector2f v1, Vector2D v2) => new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2D operator -(Vector2D v1, Vector2D v2) => new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    public static Vector2D operator -(Vector2D v1, Vector2f v2) => new Vector2D(v1.X - v1.X, v1.Y - v2.Y);
    public static Vector2D operator -(Vector2f v1, Vector2D v2) => new Vector2D(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2D operator *(Vector2D v, float m) => new Vector2D(v.X * m, v.Y * m);
    
    public static implicit operator Vector2f(Vector2D v) => new Vector2f(v.X, v.Y);
    public static explicit operator Vector2D(Vector2f v) => new Vector2D(v.X, v.Y);
}