using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace NewCar
{
    internal class Car : Drawable, Nextable
    {
        Image image;
        Sprite sprite;
        Vector2f size;

        public Car(string fileName)
        {
            image = new Image(fileName);
            sprite = new Sprite(new Texture(image));

            size = new Vector2f(100, 200);

            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f((Constants.windowWidth - size.X) / 2, 500);
            
        }

        public void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && sprite.Position.X + size.X < (Constants.windowWidth + Constants.fieldSize) / 2)
            {
                sprite.Position += new Vector2f(4, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && sprite.Position.X > (Constants.windowWidth - Constants.fieldSize) / 2)
            {
                sprite.Position += new Vector2f(-4, 0);
            }
        }

        public void Next()
        {
            Move();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite, states);
        }

    }
}
