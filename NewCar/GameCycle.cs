using SFML.Graphics;
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
        VideoMode videoMode;
        RenderWindow window;

        Frame frame;

        public GameCycle()
        {
            videoMode = new VideoMode(Constants.windowWidth, Constants.windowHeight);
            window = new RenderWindow(videoMode, "Window");

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

        public void Onclose(object sender, EventArgs e)
        {
            window.Close();
        }

    }
}
