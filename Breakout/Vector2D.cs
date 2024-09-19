using SFML.System;

namespace Breakout;

public readonly struct Vector2D(float x, float y)
{
    public static readonly Vector2D One = new(1, 1);
    public static readonly Vector2D NegativeOne = new(-1, -1);
    public static readonly Vector2D Zero = new(0, 0);
    
    public static readonly Vector2D Left = new(-1, 0);
    public static readonly Vector2D Right = new(1, 0);
    public static readonly Vector2D Up = new(0, -1);
    public static readonly Vector2D Down = new(0, 1);
    
    
    
    public float X { get; } = x;
    public float Y { get; } = y;

    public float Magnitude => MathF.Sqrt(X * X + Y * Y);
    public Vector2D Normalized => Magnitude == 0 ? Zero : new Vector2D(X / Magnitude, Y / Magnitude);

    public Vector2D(float xy) : this(xy, xy)
    {
    }

    private static Vector2D Add(Vector2D v1, Vector2D v2) => new(v1.X + v2.X, v1.Y + v2.Y);
    private static Vector2D Subtract(Vector2D v1, Vector2D v2) => new(v1.X - v2.X, v1.Y - v2.Y);
    private static Vector2D Multiply(Vector2D v, float m) => new(v.X * m, v.Y * m);
    
    public static Vector2D operator +(Vector2D v1, Vector2D v2) => Add(v1, v2);
    public static Vector2D operator +(Vector2D v1, Vector2f v2) => Add(v1, (Vector2D)v2);
    public static Vector2D operator +(Vector2f v1, Vector2D v2) => Add((Vector2D)v1, v2);
    public static Vector2D operator -(Vector2D v1, Vector2D v2) => Subtract(v1, v2);
    public static Vector2D operator -(Vector2D v1, Vector2f v2) => Subtract(v1, (Vector2D)v2);
    public static Vector2D operator -(Vector2f v1, Vector2D v2) => Subtract((Vector2D)v1, v2);

    public static Vector2D operator *(Vector2D v, float m) => Multiply(v, m);
    public static Vector2D operator *(float m, Vector2D v) => Multiply(v, m);
    
    public static implicit operator Vector2f(Vector2D v) => new(v.X, v.Y);
    public static explicit operator Vector2D(Vector2f v) => new(v.X, v.Y);
}