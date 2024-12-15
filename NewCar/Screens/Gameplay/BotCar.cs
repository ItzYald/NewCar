using NewCar.Models;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    internal class BotCar : Drawable, Nextable
    {
        Car car;
        Sprite sprite;
        Vector2f size;

        List<Nextable> nextables;
        List<Drawable> drawables;

        Func<int> getMainCarPixelDistance;

        public BotCar(string fileName, int power, int maxRpm, Func<int> getMainCarPixelDistance)
        {
            this.getMainCarPixelDistance = getMainCarPixelDistance;

            nextables = new List<Nextable>();
            drawables = new List<Drawable>();

            size = new Vector2f(300, 150);

            car = new Car(power, maxRpm);
            nextables.Add(car);

            sprite = new Sprite(new Texture(fileName));
            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 270);
            drawables.Add(sprite);
        }

        public int getPixelDistance() => (int)(car.getDistance() / 60f * 23f);

        public void Start() { car.Start(); }
        public void Stop() { car.Stop(); }

        public void Next()
        {
            if (!car.IsStarted) return;
            car.Accel();
            if (car.getRpm() > car.MaxRpm * 0.91)
            {
                car.TransmissionUp();
            }

            foreach (Nextable nextables in nextables)
            {
                nextables.Next();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            sprite.Position = new Vector2f(getPixelDistance() - getMainCarPixelDistance() + 120, sprite.Position.Y);
            target.Draw(sprite, states);
        }

    }
}
