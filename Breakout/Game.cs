using Breakout.Model;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout;

public class Game
{
    private static readonly Random _random = new Random();
    private static readonly float _scale = 0.6f;

    private readonly Texture _blueBrick;
    private readonly Texture _greenBrick;
    private readonly Texture _pinkBrick;
    
    private readonly int _nBricksX = 5;
    private readonly int _nBricksY = 4;

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
    private readonly Paddle _paddle;

    private readonly Text _scoreDisplay;
    private readonly Text _healthDisplay;

    public Game(int screenWidth, int screenHeight)
    {
        _brickOffsets = new Vector2D(200, 100) * _scale;
        _brickOrigin = new Vector2D(50, 20);
        
        Texture ballTexture = new("Assets/ball.png");
        Texture paddleTexture = new("Assets/paddle.png");
        
        _blueBrick = new Texture("Assets/brickBlue.png");
        _greenBrick = new Texture("Assets/brickGreen.png");
        _pinkBrick = new Texture("Assets/brickPink.png");

        CollidableRectangle leftWall = new CollidableRectangle(new Vector2f(10, screenHeight), new Vector2f(-10, 0));
        CollidableRectangle topWall = new CollidableRectangle(new Vector2f(screenWidth, 10), new Vector2f(0, -10));
        CollidableRectangle rightWall = new CollidableRectangle(new Vector2f(10, screenHeight), new Vector2f(screenWidth, 0));
        _walls =
        [
            leftWall,
            topWall,
            rightWall
        ];
        _hurtBox = new CollidableRectangle(new Vector2f(screenWidth, 10), new Vector2f(0, screenHeight));
        
        _ball = new Ball(ballTexture, new CircleShape(30), _scale, screenWidth, screenHeight);
        _paddle = new Paddle(paddleTexture, Vector2D.One * 0.25f, screenWidth, screenHeight, leftWall, rightWall);
        
        NewGame();
    }
    
    private void NewGame()
    {
        _score = 0;
        _health = 3;
        _timesCleared = 0;
        
        _ball.Reset();
        _paddle.Reset();
        
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
                sprite.Scale = new Vector2f(_scale, _scale);
                
                _bricks.Add(sprite);
            }
        }
    }

    private void CheckCollision(float deltaTime)
    {
        for (int i = 0; i < _bricks.Count; i++)
        {
            var brick = _bricks[i];

            if (!_ball.WillCollide(deltaTime, brick.Collider, out Vector2D brickCollisionPoint))
                continue;
            
            _ball.HandleCollision(brickCollisionPoint);
            _bricks.RemoveAt(i--);
            _score += _timesCleared + 1;
        }
        
        foreach (var wall in _walls)
        {
            if (_ball.WillCollide(deltaTime, wall, out Vector2D wallCollisionPoint))
                _ball.HandleCollision(wallCollisionPoint);
        }

        if (_ball.WillCollide(deltaTime, _paddle.Collider, out Vector2D paddleCollisionPoint))
        {
            _ball.HandleCollision(paddleCollisionPoint);
        }
        
        if (_ball.WillCollide(deltaTime, _hurtBox, out Vector2D collision))
        {
            _health--;
            _ball.Reset();
        }
    }

    public void Update(float deltaTime)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            _isGameRunning = true;
            _ball.Start();
        }

        if (!_isGameRunning)
            return;

        if (_bricks.Count == 0)
        {
            _ball.Reset();
            _paddle.Reset();
            _isGameRunning = false;

            GenerateBricks();
        }

        _paddle.Update(deltaTime);

        CheckCollision(deltaTime);
        _ball.Update(deltaTime);

        if (_health > 0)
            return;
        
        // TODO: Display a game over screen and ask if the player want to play again

        NewGame();

    }

    public void Render(RenderTarget target)
    {
        foreach (var brick in _bricks)
        {
            target.Draw(brick);
        }
        
        _ball.Draw(target);
        
        target.Draw(_paddle);
    }
}