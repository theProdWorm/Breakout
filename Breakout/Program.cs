using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(
                       new VideoMode(500, 700), "breakout"))
            {
                window.Closed += (o, e) => window.Close();
                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    float deltaTime =
                        clock.Restart().AsSeconds();
                    window.DispatchEvents();
// TODO: Updates
                    window.Clear(new Color(131, 197, 235));
// TODO: Drawing
                    window.Display();
                }
            }
        }
    }
}