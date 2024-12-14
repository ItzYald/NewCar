using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    internal class Markup : Drawable, Nextable
    {
        Sprite sprite;
        List<int> positions;

        Field.GetDelegate getCarDistance;

        public Markup(Field.GetDelegate getCarDistance)
        {
            sprite = new Sprite(new Texture("Images/Field/Markup.png"));
            sprite.Position = new Vector2f(0, 265);
            sprite.Scale = new Vector2f(2, 2);

            positions = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                positions.Add(i * 128);
            }

            this.getCarDistance = getCarDistance;

        }

        public void Next()
        {
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] = 1280 - (getCarDistance() + i * 128) % 1280 - 10;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                sprite.Position = new Vector2f(positions[i], sprite.Position.Y);
                target.Draw(sprite, states);
            }
        }
    }
}
