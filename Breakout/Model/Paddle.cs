using SFML.Graphics;
using SFML.Window;

namespace Breakout.Model;

public class Paddle : RectCollisionSprite
{
    private readonly CollidableRectangle _leftWall;
    private readonly CollidableRectangle _rightWall;

    private readonly float _speed = 400f;
    
    private readonly Vector2D _origin;

    public Vector2D Velocity { get; private set; }= Vector2D.Zero;

    public Paddle(Texture texture, Vector2D scale, int screenWidth, int screenHeight, CollidableRectangle leftWall, CollidableRectangle rightWall)
        : base(texture)
    {
        Scale = scale;
        
        _origin = new Vector2D((screenWidth - Size.X) * 0.5f, screenHeight - Size.Y - 20);    
        
        Position = _origin;
        
        _leftWall = leftWall;
        _rightWall = rightWall;
    }

    private void HandleInput()
    {
        Velocity = Vector2D.Zero;
        
        bool right = Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right);
        bool left = Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left);

        if (!(right ^ left))
            return;
        
        Velocity = (right ? Vector2D.Right : Vector2D.Left) * _speed;
    }

    public void Update(float deltaTime)
    {
        HandleInput();
        
        Vector2D nextPosition = Position + Velocity * deltaTime;

        if (nextPosition.X <= _leftWall.Position.X + _leftWall.Size.X)
        {
            Position = new Vector2D(_leftWall.Position.X + _leftWall.Size.X, Position.Y);
        }
        else if(nextPosition.X + Size.X >= _rightWall.Position.X)
        {
            Position = new Vector2D(_rightWall.Position.X - Size.X, Position.Y);
        }
        else
        {
            Position = nextPosition;
        }
    }

    public void Reset()
    {
        Velocity = Vector2D.Zero;
        Position = _origin;
    }
}