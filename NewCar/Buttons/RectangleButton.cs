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

        public RectangleButton(Vector2f position, Vector2f size, Color color, string message="") :
            base(position, size, message) 
        {
            shape = new RectangleShape(size);
            shape.Position = position;
            shape.Size = size;
            shape.FillColor = color;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape, states);
            target.Draw(text, states);
        }

    }
}
