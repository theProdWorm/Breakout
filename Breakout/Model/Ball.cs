using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class Ball : Renderable, ICollidable
{
    private Vector2f _position;
    private Vector2f _velocity;
    
    private CircleShape _circle;

    private Vector2f CircleCenter
    {
        get => _circle.Position + new Vector2f(_circle.Radius, _circle.Radius);
    }
    
    public Ball(Sprite sprite, Vector2f basePosition, Vector2f baseVelocity, CircleShape circle) : base(sprite)
    {
        _position = basePosition;
        _velocity = baseVelocity;
        _circle = circle;
    }

    public void CheckCollision(CollidableRectangle other)
    {
        RectangleShape rectangle = new RectangleShape();
        rectangle.Size = new Vector2f(_circle.Radius * 2, _circle.Radius * 2);
        rectangle.Position = _circle.Position;
        if (other.Intersects(rectangle) && CircleIntersects(rectangle))
        {
            HandleCollision();
            other.HandleCollision();
        }
    }

    private bool CircleIntersects(RectangleShape other)
    {
        if (GetDistanceF(CircleCenter, other.Position) <= _circle.Radius || //Check rectangle top left corner intersect
            GetDistanceF(CircleCenter, other.Position + new Vector2f(other.Size.X, 0)) <= _circle.Radius || //Check rectangle top right corner intersect
            GetDistanceF(CircleCenter, other.Position + new Vector2f(0, other.Size.Y)) <= _circle.Radius || //Check rectangle bottom left corner intersect
            GetDistanceF(CircleCenter, other.Position + other.Size) <= _circle.Radius) //Check rectangle bottom right corner intersect
            return true;
        
        //TODO: Check non-corner intersects (lines)
        throw new NotImplementedException();
    }

    private Vector2f GetDistance(Vector2f position1, Vector2f position2)
    {
        Vector2f distance = position2 - position1;
        distance.X = MathF.Abs(distance.X);
        distance.Y = MathF.Abs(distance.Y);
        return distance;
    }

    private float GetDistanceF(Vector2f position1, Vector2f position2)
    {
        Vector2f distance = GetDistance(position1, position2);
        float distanceF = MathF.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
        return distanceF;
    }

    public void HandleCollision()
    {
        throw new NotImplementedException();
    }
}