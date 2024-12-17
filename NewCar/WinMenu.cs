using NewCar.Buttons;
using NewCar.Screens;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar
{
    internal class WinMenu : Drawable, Nextable
    {
        RectangleShape shape;

        List<Nextable> nextables;
        List<Drawable> drawables;

        List<RectangleButton> buttons;

        Action restart;
        Action backToMainMenu;

        public WinMenu(Action restart, Action backToMainMenu)
        {
            nextables = new List<Nextable>();
            drawables = new List<Drawable>();

            shape = new RectangleShape(new Vector2f(300, 250));
            shape.Position = new Vector2f((Constants.windowWidth - shape.Size.X) / 2, 300);
            shape.FillColor = new Color(0, 100, 0);
            drawables.Add(shape);

            buttons = new List<RectangleButton>();

            this.restart = restart;
            this.backToMainMenu = backToMainMenu;

            buttons.Add(new RectangleButton(
                new Vector2f((Constants.windowWidth - 120) / 2, 320),
                new Vector2f(120, 60), "Заново")
            {
                onClick = () => { this.restart(); }
            });
            nextables.Add(buttons[buttons.Count - 1]);
            drawables.Add(buttons[buttons.Count - 1]);

        }

        public virtual void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                target.Draw(drawable, states);
            }
        }

    }
}
