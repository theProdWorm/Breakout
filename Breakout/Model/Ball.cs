using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class Ball : Sprite
{
    private readonly CircleShape _circle;
    private readonly Vector2D _origin;

    private readonly float _speed = 200f;
    
    private Vector2D _velocity = Vector2D.Zero;
    
    private Vector2D CircleCenter => _circle.Position + Vector2D.One * _circle.Radius;

    public Ball(Texture texture, CircleShape circle, float scale, int screenWidth, int screenHeight) : base(texture)
    {
        circle.Radius *= scale;
        _circle = circle;
        
        float scaleFactor = _circle.Radius * 2 / texture.Size.X;
        Scale = new Vector2D(scaleFactor);

        _origin = new Vector2D((screenWidth - _circle.Radius * 2f) * 0.5f, screenHeight - 95);
        Position = _origin;
    }

    public void Start()
    {
        _velocity = Vector2D.NegativeOne * _speed;
    }
    
    public void Update(float deltaTime)
    {
        Position += _velocity * deltaTime;
        _circle.Position = Position;
    }

    public bool WillCollide(float deltaTime, CollidableRectangle other, out Vector2D collisionNormal)
    {
        Vector2D nextPosition = Position + _velocity * deltaTime;
        Vector2D projectedCircleCenter = nextPosition + Vector2D.One * _circle.Radius;
        
        collisionNormal = Vector2D.Zero;
        
        if ((projectedCircleCenter - other.Position).Magnitude <= _circle.Radius) //Check rectangle top left corner intersect
        {
            collisionNormal = Vector2D.Up + Vector2D.Left;
            return true;
        }
        if ((projectedCircleCenter - (other.Position + new Vector2D(other.Size.X * other.Scale.X, 0))).Magnitude <= _circle.Radius) //Check rectangle top right corner intersect
        {
            collisionNormal = Vector2D.Up + Vector2D.Right;
            return true;
        }
        if ((projectedCircleCenter - (other.Position + new Vector2D(0, other.Size.Y * other.Scale.Y))).Magnitude <= _circle.Radius) //Check rectangle bottom left corner intersect
        {
            collisionNormal = Vector2D.Down + Vector2D.Left;
            return true;
        }
        if ((projectedCircleCenter - (other.Position + new Vector2D(other.Size.X * other.Scale.X, other.Size.Y * other.Scale.Y))).Magnitude <= _circle.Radius) //Check rectangle bottom right corner intersect
        {
            collisionNormal = Vector2D.Down + Vector2D.Right;
            return true;
        }

        if (other.ContainsPoint(projectedCircleCenter + Vector2D.Up * _circle.Radius)) //Check _circle topmost point intersect
        {
            collisionNormal = Vector2D.Up;
            return true;
        }
        if (other.ContainsPoint(projectedCircleCenter + Vector2D.Right * _circle.Radius)) //Check _circle rightmost point intersect
        {
            collisionNormal = Vector2D.Right;
            return true;
        }

        if (other.ContainsPoint(projectedCircleCenter + Vector2D.Down * _circle.Radius)) //Check _circle bottommost point intersect
        {
            collisionNormal = Vector2D.Down;
            return true;
        }
        if (other.ContainsPoint(projectedCircleCenter + Vector2D.Left * _circle.Radius)) //Check _circle leftmost point intersect
        {
            collisionNormal = Vector2D.Left;
            return true;
        }

        return false;
    }

    public void HandleCollision(Vector2D collisionNormal)
    {
        if(collisionNormal.X != 0)
            _velocity = new Vector2D(-_velocity.X, _velocity.Y);
        if(collisionNormal.Y != 0)
            _velocity = new Vector2D(_velocity.X, -_velocity.Y);
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(this);
        // _circle.FillColor = Color.Red;
        // target.Draw(_circle);
    }

    public void Reset()
    {
        _velocity = Vector2D.Zero;
        Position = _origin;
    }
}