using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class Ball : Sprite
{
    private readonly CircleShape _circle;
    private Vector2D _velocity = Vector2D.Zero;

    private Vector2D CircleCenter => _circle.Position + new Vector2D(_circle.Radius);

    public Ball(Texture texture, CircleShape circle, Vector2D position) : base(texture)
    {
        _circle = circle;
        float scaleFactor = _circle.Radius * 2 / texture.Size.X;
        Scale = new Vector2D(scaleFactor);
        Position = position;

        _velocity = new Vector2D(100, -100); //Debug line remove in production
    }

    public void Update(float deltaTime)
    {
        Position += _velocity * deltaTime;
        _circle.Position = Position;
    }

    public bool IsColliding(CollidableRectangle other, out Vector2D collisionPoint) //TODO: out collisionpoint
    {
        collisionPoint = Vector2D.Zero;
        
        if ((CircleCenter - other.Position).Magnitude <= _circle.Radius) //Check rectangle top left corner intersect
        {
            collisionPoint = (Vector2D)other.Position;
            return true;
        }
        if ((CircleCenter - (other.Position + new Vector2D(other.Size.X * other.Scale.X, 0))).Magnitude <= _circle.Radius) //Check rectangle top right corner intersect
        {
            collisionPoint = (Vector2D)other.Position + new Vector2D(other.Size.X * other.Scale.X, 0);
            return true;
        }
        if ((CircleCenter - (other.Position + new Vector2D(0, other.Size.Y * other.Scale.Y))).Magnitude <= _circle.Radius) //Check rectangle bottom left corner intersect
        {
            collisionPoint = (Vector2D)other.Position + new Vector2D(0, other.Size.Y * other.Scale.Y);
            return true;
        }
        if ((CircleCenter - (other.Position + new Vector2D(other.Size.X * other.Scale.X, other.Size.Y * other.Scale.Y))).Magnitude <= _circle.Radius) //Check rectangle bottom right corner intersect
        {
            collisionPoint = other.Position + new Vector2D(other.Size.X * other.Scale.X, other.Size.Y * other.Scale.Y);
            return true;
        }

        if (other.ContainsPoint(CircleCenter + Vector2D.Up * _circle.Radius)) //Check _circle topmost point intersect
        {
            collisionPoint = CircleCenter + Vector2D.Up * _circle.Radius;
            return true;
        }
        if (other.ContainsPoint(CircleCenter + Vector2D.Right * _circle.Radius)) //Check _circle rightmost point intersect
        {
            collisionPoint = CircleCenter + Vector2D.Right * _circle.Radius;
            return true;
        }

        if (other.ContainsPoint(CircleCenter + Vector2D.Down * _circle.Radius)) //Check _circle bottommost point intersect
        {
            collisionPoint = CircleCenter + Vector2D.Down * _circle.Radius;
            return true;
        }
        if (other.ContainsPoint(CircleCenter + Vector2D.Left * _circle.Radius)) //Check _circle leftmost point intersect
        {
            collisionPoint = CircleCenter + Vector2D.Left * _circle.Radius;
            return true;
        }

        return false;
    }

    public void HandleCollision(Vector2D collisionPoint)
    {
        Vector2D normal = CircleCenter - collisionPoint; //TODO: normalize to magnitude 1
        float angle = MathF.Atan2(_velocity.X, _velocity.Y) - MathF.Atan2(normal.X, normal.Y);
        float newangle = MathF.Atan2(_velocity.X, _velocity.Y) + angle * 2;
        _velocity = new Vector2D(MathF.Cos(newangle) * _velocity.Magnitude, MathF.Sin(newangle) * _velocity.Magnitude);
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(this);
        // _circle.FillColor = Color.Red;
        // target.Draw(_circle);
    }
}