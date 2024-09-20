using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class Ball : Sprite
{
    private readonly CircleShape _circle;
    private readonly Vector2D _startingPosition;

    private readonly float _speed = 200f;
    
    private Vector2D _velocity = Vector2D.Zero;
    
    private Vector2D CircleCenter => _circle.Position + Vector2D.One * _circle.Radius;

    public Ball(Texture texture, CircleShape circle, float scale, Vector2D startingPosition) : base(texture)
    {
        circle.Radius *= scale;
        _circle = circle;
        
        float scaleFactor = _circle.Radius * 2 / texture.Size.X;
        Scale = new Vector2D(scaleFactor);

        _startingPosition = startingPosition;
        Position = _startingPosition;
    }

    public void Start()
    {
        Vector2D velocity = new Vector2D(Game._random.Next(2) == 0 ? -1 : 1, -1);
        velocity = velocity.Normalized * _speed;

        _velocity = velocity;
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

    public void HandleWillCollide(Vector2D collisionNormal)
    {
        if(collisionNormal.X != 0)
            _velocity = new Vector2D(-_velocity.X, _velocity.Y);
        if(collisionNormal.Y != 0)
            _velocity = new Vector2D(_velocity.X, -_velocity.Y);
    }

    public void HandlePaddleWillCollide(Vector2D paddleVelocity, Vector2D paddlePosition)
    {
        Vector2D newVelocity = _velocity.Normalized;
        newVelocity += paddleVelocity.Normalized;

        newVelocity = newVelocity.Normalized * _speed;
        
        _velocity = newVelocity;

        Position = new Vector2f(Position.X, paddlePosition.Y - _circle.Radius * 2);
        _circle.Position = Position;
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
        Position = _startingPosition;
    }
}