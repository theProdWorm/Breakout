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
        if (other.Intersects(rectangle) && CircleIntersects(other))
        {
            HandleCollision();
            other.HandleCollision();
        }
    }

    private bool CircleIntersects(CollidableRectangle other) //TODO: out collisionpoint? 
    {
        if (GetDistanceF(CircleCenter, other.Rectangle.Position) <= _circle.Radius || //Check rectangle top left corner intersect
            GetDistanceF(CircleCenter, other.Rectangle.Position + new Vector2f(other.Rectangle.Size.X, 0)) <= _circle.Radius || //Check rectangle top right corner intersect
            GetDistanceF(CircleCenter, other.Rectangle.Position + new Vector2f(0, other.Rectangle.Size.Y)) <= _circle.Radius || //Check rectangle bottom left corner intersect
            GetDistanceF(CircleCenter, other.Rectangle.Position + other.Rectangle.Size) <= _circle.Radius) //Check rectangle bottom right corner intersect
            return true;
        
        if (other.ContainsPoint(CircleCenter + new Vector2f(0, _circle.Radius * -1)) || //Check circle topmost point intersect
            other.ContainsPoint(CircleCenter + new Vector2f(_circle.Radius, 0)) || //Check circle rightmost point intersect
            other.ContainsPoint(CircleCenter + new Vector2f(0, _circle.Radius)) || //Check circle bottommost point intersect
            other.ContainsPoint(CircleCenter + new Vector2f(_circle.Radius * -1, 0))) //Check circle leftmost point intersect
            return true;

        return false;
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