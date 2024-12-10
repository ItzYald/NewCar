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
        RectangleShape mainRect;

        public Field()
        {
            mainRect = new RectangleShape();
            mainRect.Size = new Vector2f(Constants.fieldSize + 50, 1280);
            mainRect.Position = new Vector2f((Constants.windowWidth - mainRect.Size.X) / 2, 0);
            mainRect.FillColor = new Color(100, 100, 100);
        }

        public void Next()
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(mainRect, states);
        }

    }
}
