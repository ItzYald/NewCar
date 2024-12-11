using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace NewCar
{
    internal class Car : Drawable, Nextable
    {
        Sprite sprite;
        Vector2f size;


        int distance;
        float speed;
        int rpm;

        float[] numberOfTransmissions;

        int transmissionNumber;

        public Car(string fileName)
        {
            sprite = new Sprite(new Texture(fileName));

            size = new Vector2f(200, 100);

            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 220);

            distance = 0;
            speed = 0;
            rpm = 1000;

            numberOfTransmissions = new float[] { 1f, 1.2f, 1.4f, 1.7f };
            transmissionNumber = 0;
        }

        public int getDistance() => distance;
        public int getPixelDistance() => (int)(distance / 60f * 23f);
        public int getSpeed() => (int)speed;
        public int getRpm() => rpm;

        void Accel()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                rpm += 30;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && speed > 0)
            {

                rpm -= 30;
            }
            speed = rpm / 100f * numberOfTransmissions[transmissionNumber];
        }

        public void Next()
        {
            Accel();
            distance += (int)speed;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite, states);
        }

    }
}
