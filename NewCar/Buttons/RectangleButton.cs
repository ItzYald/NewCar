using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Buttons
{
    internal class RectangleButton : BaseButton
    {
        RectangleShape shape;
        Color fillColor;

        //button = new RectangleButton(new Vector2f(10, 10), new Vector2f(100, 50), new Color(230, 230, 30))
        //{
        //    onClick = () => { engine.rpm = 100; }
        //};
        //drawables.Add(button);
        //nextables.Add(button);

        public RectangleButton(Vector2f position, Vector2f size, Color color) :
            base(position, size) 
        {
            shape = new RectangleShape(size);
            shape.Position = position;
            shape.Size = size;
            shape.FillColor = color;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape, states);
            if (text != null)
            {
                target.Draw(text, states);
            }
        }

    }
}
