using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class Ball(Texture texture, CircleShape circle) : Sprite(texture)
{
    private readonly CircleShape _circle = circle;
    private Vector2D _velocity = Vector2D.Zero;

    private Vector2D CircleCenter => _circle.Position + new Vector2D(_circle.Radius);

    public bool IsColliding(CollidableRectangle other) //TODO: out collisionpoint? 
    {
        if ((CircleCenter - other.Position).Magnitude <= _circle.Radius || //Check rectangle top left corner intersect
            (CircleCenter - (other.Position + new Vector2D(other.Size.X, 0))).Magnitude <= _circle.Radius || //Check rectangle top right corner intersect
            (CircleCenter - (other.Position + new Vector2D(0, other.Size.Y))).Magnitude <= _circle.Radius || //Check rectangle bottom left corner intersect
            (CircleCenter - (other.Position + other.Size)).Magnitude <= _circle.Radius) //Check rectangle bottom right corner intersect
            return true;
        
        if (other.ContainsPoint(CircleCenter + Vector2D.Up * _circle.Radius) || //Check _circle topmost point intersect
            other.ContainsPoint(CircleCenter + Vector2D.Right * _circle.Radius) || //Check _circle rightmost point intersect
            other.ContainsPoint(CircleCenter + Vector2D.Down * _circle.Radius) || //Check _circle bottommost point intersect
            other.ContainsPoint(CircleCenter + Vector2D.Left * _circle.Radius)) //Check _circle leftmost point intersect
            return true;

        return false;
    }
}