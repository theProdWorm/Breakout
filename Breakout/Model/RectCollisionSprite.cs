using SFML.Graphics;
using SFML.System;

namespace Breakout.Model;

public class RectCollisionSprite(Texture texture) : Sprite(texture)
{
    public CollidableRectangle Collider { get; } = new((Vector2f) texture.Size);
}