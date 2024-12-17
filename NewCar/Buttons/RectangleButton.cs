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
    internal struct ButtonColors
    {
        public Color fillCollor;
        public Color outlineColor;
        public float outlineSize;

        public ButtonColors(Color fillColor, Color outlineColor, float outlineSize)
        {
            this.fillCollor = fillColor;
            this.outlineColor = outlineColor;
            this.outlineSize = outlineSize;
        }
    }

    internal class RectangleButton : BaseButton, ICloneable
    {
        RectangleShape shape;

        public RectangleButton(Vector2f position, Vector2f size, Color fillColor, string message, Color outlineColor, float outlineSize) :
            base(position, size, message)
        {
            shape = new RectangleShape(size);
            shape.Position = position;
            shape.Size = size;
            shape.FillColor = fillColor;
            shape.OutlineColor = outlineColor;
            shape.OutlineThickness = outlineSize;
        }

        public RectangleButton(Vector2f position, Vector2f size, Color fillColor, string message, Color outlineColor) :
            this(position, size, fillColor, "", outlineColor, 2) { }

        public RectangleButton(Vector2f position, Vector2f size, Color fillColor, string message) :
            this(position, size, fillColor, "", Color.Transparent) { }

        public RectangleButton(Vector2f position, Vector2f size, Color fillColor) :
            this(position, size, fillColor, "") { }

        public RectangleButton(Vector2f position, Vector2f size, string message) :
            this(position, size, Constants.baseButtonColors.fillCollor,
                message, Constants.baseButtonColors.outlineColor, Constants.baseButtonColors.outlineSize) { }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape, states);
            target.Draw(text, states);
        }

        public object Clone()
        {
            return new RectangleButton(position, size, shape.FillColor, text.DisplayedString, shape.OutlineColor, shape.OutlineThickness);
        }
    }
}
