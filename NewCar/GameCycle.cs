using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar
{
    internal class GameCycle
    {
        static VideoMode videoMode = new VideoMode(Constants.windowWidth, Constants.windowHeight);
        static RenderWindow window = new RenderWindow(videoMode, "Window");

        Frame frame;

        public GameCycle()
        {
            window.Closed += Onclose;

            window.SetFramerateLimit(60);

            frame = new Frame();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                frame.Next();
                window.Draw(frame);

                window.Display();
            }
        }
         
        public static Vector2i GetMousePosition()
        {
            return Mouse.GetPosition(window);
        }
        private void Onclose(object? sender, EventArgs e)
        {
            window.Close();
        }

    }
}
