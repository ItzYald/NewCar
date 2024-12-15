using SFML.Graphics;
using SFML.Window;
using SFML.System;
using NewCar.Buttons;
using NewCar.Models;



namespace NewCar.Screens.Gameplay
{
    internal class PlayerCar : Drawable, Nextable
    {
        Car car;
        Sprite sprite;
        Vector2f size;

        List<Nextable> nextables;
        List<Drawable> drawables;

        Speedometer speedometer;

        public PlayerCar(string fileName, int power, int maxRpm)
        {
            nextables = new List<Nextable>();
            drawables = new List<Drawable>();
            size = new Vector2f(300, 150);

            sprite = new Sprite(new Texture(fileName));
            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 130);
            drawables.Add(sprite);

            car = new Car(power, maxRpm);
            nextables.Add(car);

            speedometer = new Speedometer(car.getSpeed, car.getRpm, car.getTransmissionNumber);
            drawables.Add(speedometer);
            nextables.Add(speedometer);

            nextables.Add(new CheckKey(Keyboard.Key.K, car.TransmissionDown));
            nextables.Add(new CheckKey(Keyboard.Key.L, car.TransmissionUp));
        }

        public void Start() { car.Start(); }
        public void Stop() { car.Stop(); }

        public int getPixelDistance() => (int)(car.getDistance() / 60f * 23f);

        public void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
            if (!car.IsStarted) return;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                car.Accel();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                car.Break();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                drawable.Draw(target, states);
            }
        }

    }
}
