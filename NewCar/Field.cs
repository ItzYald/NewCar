using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar
{
    internal class Field : Drawable, Nextable
    {
        RectangleShape backGround;
        RectangleShape asphalt;
        Markup markup;

        List<Drawable> drawables;

        GetDelegate getCarDistance;

        public delegate int GetDelegate();

        public Field(GetDelegate getCarDistance)
        {
            drawables = new List<Drawable>();

            backGround = new RectangleShape();
            backGround.Size = new Vector2f(1280, 720);
            backGround.Position = new Vector2f(0, 0);
            backGround.FillColor = new Color(10, 180, 10);
            drawables.Add(backGround);

            asphalt = new RectangleShape();
            asphalt.Size = new Vector2f(1280, 100);
            asphalt.Position = new Vector2f(0, 220);
            asphalt.FillColor = new Color(100, 100, 100);
            drawables.Add(asphalt);

            markup = new Markup(getCarDistance);
            drawables.Add(markup);

            this.getCarDistance = getCarDistance;
        }

        public void Next()
        {
            markup.Next();
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
