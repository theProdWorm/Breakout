using Breakout.Model;
using SFML.Graphics;
using SFML.System;

namespace Breakout;

public class Game
{
    private static readonly Random _random = new Random();

    private readonly Texture _blueBrick;
    private readonly Texture _greenBrick;
    private readonly Texture _pinkBrick;
    
    private readonly int _nBricksX = 5;
    private readonly int _nBricksY = 4;

    private readonly float _brickScale = 0.6f;
    
    private readonly Vector2D _brickOffsets;
    private readonly Vector2D _brickOrigin;
    
    private readonly List<RectCollisionSprite> _bricks = [];

    private readonly List<CollidableRectangle> _walls;
    private readonly CollidableRectangle _hurtBox;
    
    private int _score;
    private int _health;
    private int _timesCleared;

    private bool _isGameRunning;

    private readonly Ball _ball;
    private readonly Sprite _paddle;

    private readonly Text _scoreDisplay;
    private readonly Text _healthDisplay;

    public Game(int screenwidth, int screenheight)
    {
        _brickOffsets = new Vector2D(200, 100) * _brickScale;
        _brickOrigin = new Vector2D(50, 20);
        
        Texture ballTexture = new("Assets/ball.png");
        Texture paddleTexture = new("Assets/paddle.png");
        
        _blueBrick = new Texture("Assets/brickBlue.png");
        _greenBrick = new Texture("Assets/brickGreen.png");
        _pinkBrick = new Texture("Assets/brickPink.png");

        CollidableRectangle leftWall = new CollidableRectangle(new Vector2f(10, screenheight), new Vector2f(-10, 0));
        CollidableRectangle topWall = new CollidableRectangle(new Vector2f(screenwidth, 10), new Vector2f(0, -10));
        CollidableRectangle rightWall = new CollidableRectangle(new Vector2f(10, screenheight), new Vector2f(screenwidth, 0));
        _walls =
        [
            leftWall,
            topWall,
            rightWall
        ];
        
        _hurtBox = new CollidableRectangle(new Vector2f(screenwidth, 10), new Vector2f(0, screenheight));
        
        _ball = new Ball(ballTexture, new CircleShape(30), new Vector2D(70, 400));

        NewGame();
    }
    
    private void NewGame()
    {
        _score = 0;
        _health = 3;
        _timesCleared = 0;
        
        _isGameRunning = false;
        
        GenerateBricks();
    }

    private void GenerateBricks()
    {
        for (int y = 0; y < _nBricksY; y++)
        {
            for (int x = 0; x < _nBricksX; x++)
            {
                int randomInt = _random.Next(3);
                Texture randomizedBrick = randomInt switch
                {
                    0 => _blueBrick,
                    1 => _greenBrick,
                    2 => _pinkBrick,
                    _ => throw new ArgumentOutOfRangeException()
                };

                RectCollisionSprite sprite = new(randomizedBrick);
                sprite.Position = new Vector2f(x * _brickOffsets.X, y * _brickOffsets.Y) + _brickOrigin;
                sprite.Scale = new Vector2f(_brickScale, _brickScale);
                
                _bricks.Add(sprite);
            }
        }
    }

    private void CheckCollision()
    {
        RectCollisionSprite? remove = null;
        foreach (var brick in _bricks)
        {
            if (_ball.IsColliding(brick.Collider, out Vector2D collisionPoint))
            {
                _ball.HandleCollision(collisionPoint);
                remove = brick;
            }
            
        }
        if (remove is not null)
        {
            _bricks.Remove(remove);
            //TODO: increase score
        }

        foreach (var wall in _walls)
        {
            if (_ball.IsColliding(wall, out Vector2D collisionPoint))
                _ball.HandleCollision(collisionPoint);
        }

        if (_ball.IsColliding(_hurtBox, out Vector2D collision))
        {
            _ball.HandleCollision(collision);
            //TODO: remove hp, reset ball
        }
    }
    
    public void Update(float deltaTime)
    {
        if(_bricks.Count == 0)
            GenerateBricks();
        
        _ball.Update(deltaTime);
        CheckCollision();
    }

    public void Render(RenderTarget target)
    {
        foreach (var brick in _bricks)
        {
            target.Draw(brick);
        }
        _ball.Draw(target);
    }
}