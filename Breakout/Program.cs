using Breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

var window = new RenderWindow(
    new VideoMode(700, 500), "breakout");

window.Closed += (o, e) => window.Close();

Game game = new();
Clock clock = new Clock();
while (window.IsOpen)
{
    float deltaTime =
        clock.Restart().AsSeconds();
    
    window.DispatchEvents();

    game.Update(deltaTime);
    window.Clear(new Color(131, 197, 235));

    game.Render(window);
    window.Display();
}