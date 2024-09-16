using SFML.Graphics;

namespace Breakout.Model;

public abstract class Renderable
{
    private Sprite _sprite;

    public Renderable(Sprite sprite)
    {
        _sprite = sprite;
    }

    public virtual void Update(float deltaTime)
    {
        
    }

    public virtual void Draw(RenderTarget target)
    {
        target.Draw(_sprite);
    }
}