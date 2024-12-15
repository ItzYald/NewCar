using SFML.Graphics;
using SFML.Window;
using SFML.System;
using NewCar.Buttons;
using NewCar.Models;



namespace NewCar.Screens.Gameplay
{
    internal class DrawCar : Drawable, Nextable
    {
        Car car;
        Sprite sprite;
        Vector2f size;

        List<Nextable> nextables;
        List<Drawable> drawables;

        Speedometer speedometer;

        public DrawCar(string fileName, int power, int maxRpm)
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
        }

        public int getPixelDistance() => (int)(car.getDistance() / 60f * 23f);

        public void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
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
