using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar
{
    internal class Speedometer : Drawable, Nextable
    {
        GetDelegate getSpeed;
        GetDelegate getRpm;

        Text speedText;
        Text rpmText;

        RectangleShape speedRect;
        RectangleShape rpmRect;

        List<Drawable> drawables;

        public delegate int GetDelegate();

        public Speedometer(GetDelegate getSpeed, GetDelegate getRpm)
        {
            drawables = new List<Drawable>();

            speedText = new Text(getSpeed().ToString(), new Font("Fonts/arial.ttf"), 25);
            speedText.Position = new Vector2f(100, 500);
            speedText.FillColor = new Color(250, 250, 10);
            rpmText = new Text(getRpm().ToString(), new Font("Fonts/arial.ttf"), 25);
            rpmText.Position = new Vector2f(500, 500);
            rpmText.FillColor = new Color(250, 250, 10);

            speedRect = new RectangleShape(new Vector2f(10, 100));
            speedRect.FillColor = new Color(250, 250, 10);
            speedRect.Position = new Vector2f(120, 450);
            

            drawables.Add(speedText);
            drawables.Add(rpmText);
            drawables.Add(speedRect);

            this.getSpeed = getSpeed;
            this.getRpm = getRpm;

        }

        public void Next()
        {
            speedText.DisplayedString = getSpeed().ToString();
            rpmText.DisplayedString = getRpm().ToString();
            speedRect.Rotation = getSpeed() / 1.2f + 80;
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
