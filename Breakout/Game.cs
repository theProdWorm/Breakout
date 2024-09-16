using System.Numerics;
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
    
    private readonly Vector2f _brickOffsets;
    private readonly Vector2f _brickOrigin;
    
    private readonly List<Sprite> _bricks = [];
    
    private int _score;
    private int _health;

    private bool _isGameRunning;

    private readonly Sprite _ball;
    private readonly Sprite _paddle;

    private readonly Text _scoreDisplay;
    private readonly Text _healthDisplay;

    public Game()
    {
        _brickOffsets = new Vector2f(200, 100) * _brickScale;
        _brickOrigin = new Vector2f(50, 20);
        
        Texture ballTexture = new("Assets/ball.png");
        Texture paddleTexture = new("Assets/paddle.png");
        
        _blueBrick = new Texture("Assets/brickBlue.png");
        _greenBrick = new Texture("Assets/brickGreen.png");
        _pinkBrick = new Texture("Assets/brickPink.png");
        
        _ball = new Sprite(ballTexture);

        NewGame();
    }
    
    private void NewGame()
    {
        _score = 0;
        _health = 3;
        
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

                Sprite sprite = new(randomizedBrick);
                sprite.Position = new Vector2f(x * _brickOffsets.X, y * _brickOffsets.Y) + _brickOrigin;
                sprite.Scale = new Vector2f(_brickScale, _brickScale);
                
                _bricks.Add(sprite);
            }
        }
    }
    
    public void Update(float deltaTime)
    {
        
    }

    public void Render(RenderTarget window)
    {
        foreach (var brick in _bricks)
        {
            window.Draw(brick);
        }
    }
}