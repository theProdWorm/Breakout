using Breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

var window = new RenderWindow(
    new VideoMode(700, 500), "breakout");

window.Closed += (o, e) => window.Close();

Game game = new(700, 500, window.Close);
Clock clock = new Clock();
while (window.IsOpen)
{
    float deltaTime =
        clock.Restart().AsSeconds();
    
    window.DispatchEvents();

    game.Update(deltaTime);
    window.Clear(new Color(0, 40, 80));

    game.Render(window);
    window.Display();
}