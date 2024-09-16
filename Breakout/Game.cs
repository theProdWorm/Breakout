using SFML.Graphics;

namespace Breakout;

public class Game
{
    private int _score;
    private int _health;

    private bool _isGameRunning;

    private Sprite _leftWall, _rightWall, _topWall;
    
    private Sprite _ball;
    private Sprite _paddle;
    private List<Sprite> _bricks;

    private Text _scoreDisplay;
    private Text _healthDisplay;

    public Game()
    {
        Texture _ballTexture = new("Assets/ball.png");
        _ball = new Sprite(_ballTexture);
        
        NewGame();
    }
    
    private void NewGame()
    {
        _score = 0;
        _health = 3;
        
        _isGameRunning = false;
    }

    private void GenerateBricks()
    {
        
    }
    
    public void Update(float deltaTime)
    {
        
    }

    public void Render(RenderTarget window)
    {
    }
}