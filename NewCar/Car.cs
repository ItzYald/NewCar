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

        public Car(string fileName)
        {
            sprite = new Sprite(new Texture(fileName));

            size = new Vector2f(200, 100);

            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 220);

            distance = 0;
            speed = 0;

        }

        public int getDistance()
        {
            return distance;
        }

        void Accel()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                speed += 0.2f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && speed > 0)
            {
                
                speed -= 0.2f;
            }
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
